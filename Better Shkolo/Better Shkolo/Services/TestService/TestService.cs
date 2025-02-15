﻿using AutoMapper;
using BetterShkolo.Data;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.Test;
using BetterShkolo.Services.AccountService;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using static BetterShkolo.Data.Constants;
using static System.Net.Mime.MediaTypeNames;

namespace BetterShkolo.Services.TestService
{
    public class TestService : ITestService
    {
        private ApplicationDbContext context;
        private IAccountService accountService;
        private IMapper mapper;
        public TestService(ApplicationDbContext context, IAccountService accountService, IMapper mapper)
        {
            this.context = context;
            this.accountService = accountService;
            this.mapper = mapper;
        }
        public async Task<bool> Add(TestAddModel model)
        {
            var subject = await context.Subjects.FindAsync(model.SubjectId);

            if (subject is null)
            {
                return false;
            }

            var test = mapper.Map<Test>(subject);
            test.AddedOn = DateTime.Now;
            test.SubjectId = subject.Id;
            test.TestDate = model.TestDate;
            test.Type = model.Type;
            test.Id = 0;
            test.Period = model.Period;
            test.Term = 1;

            if (test.Type == "Контролно")
            {
                var count = 0;

                foreach (var t in context.Tests.Where(x => x.GradeId == test.GradeId))
                {
                    if (t.TestDate.Day == test.TestDate.Day)
                    {
                        count++;
                    }

                    if (count >= 2)
                    {
                        return false;
                    }
                }
            }

            if (test.Type == "Класно")
            {
                foreach (var t in context.Tests.Where(x => x.GradeId == test.GradeId))
                {
                    int firstDayOfWeek = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                    DateTime startOfWeek = t.TestDate.Date.AddDays(-(int)t.TestDate.DayOfWeek + firstDayOfWeek);
                    DateTime endOfWeek = startOfWeek.AddDays(6);

                    bool isSameWeek = startOfWeek <= test.TestDate && test.TestDate <= endOfWeek;

                    if (isSameWeek)
                    {
                        t.TestDate.AddDays(7);
                        await context.SaveChangesAsync();
                    }
                }
            }

            await context.Tests.AddAsync(test);
            await context.SaveChangesAsync();

            return await context.Tests.ContainsAsync(test);
        }

        public async Task<TestViewScheduleModel> GetSchedule(int gradeId, int week)
        {
            var all = new List<TestScheduleModel>();

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            var gradeTests = await context.Tests
                .Where(x => x.GradeId == gradeId)
                .ToListAsync();

            foreach (var test in gradeTests)
            {
                if (cal.GetWeekOfYear(test.TestDate, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == week)
                {
                    var s = await context.Subjects.FindAsync(test.SubjectId);
                    var t = await context.Teachers.FindAsync(test.TeacherId);
                    var tU = await context.Users.FindAsync(t.UserId);
                    var tst = new TestScheduleModel()
                    {
                        Id = test.Id,
                        SubjectId = test.SubjectId,
                        SubjectName = s.Name,
                        TestDate = test.TestDate,
                        DateWeekDay = test.TestDate.DayOfWeek.ToString(),
                        Period = test.Period,
                        DateWeekDayNumber = (int)test.TestDate.DayOfWeek,
                        TeacherName = tU.FirstName + " " + tU.LastName,
                        Week = week
                    };

                    all.Add(tst);
                }
            }

            var weekForward = week + 1;

            if (weekForward > 52)
            {
                weekForward = 1;
            }

            var weekBack = week - 1;

            if (weekBack <= 0)
            {
                weekBack = 52;
            }

            var model = new TestViewScheduleModel()
            {
                Tests = all,
                GradeId = gradeId,
                WeekBack = weekBack,
                WeekForward = weekForward,
            };

            return model;
        }

        public async Task<List<TestDisplayModel>> GetTests()
        {
            var userId = accountService.GetUserId();

            var model = new List<TestDisplayModel>();

            var student = await context.Students.FirstOrDefaultAsync(x => x.UserId == userId);

            if (student == null)
            {
                return new List<TestDisplayModel>();
            }

            var studentId = 0;

            if (student == null)
            {
                var parent = await context.Parents.FirstOrDefaultAsync(x => x.UserId == userId);

                studentId = parent.StudentId;
            }
            else
            {
                studentId = student.Id;
            }

            if (studentId == 0)
            {
                return null;
            }

            student = await context.Students.FindAsync(studentId);

            var tests = await context.Tests.Where(x => x.GradeId == student.GradeId).ToListAsync();

            foreach (var test in tests)
            {
                var subjectId = test.SubjectId;

                if (!model.Any(x => x.SubjectId == subjectId))
                {
                    var subject = await context.Subjects.FindAsync(subjectId);

                    model.Add(new TestDisplayModel()
                    {
                        SubjectId = subjectId,
                        SubjectName = subject.Name,
                        Tests = new List<TestViewModel>()
                    });
                }

                var teacher = await context.Teachers.FindAsync(test.TeacherId);
                var teacherUser = await context.Users.FindAsync(teacher.UserId);

                model.FirstOrDefault(x => x.SubjectId == subjectId).Tests.Add(new TestViewModel()
                {
                    Id = test.Id,
                    TestDate = test.TestDate,
                    TeacherId = teacher.Id,
                    TeacherName = teacherUser.FirstName + " " + teacherUser.LastName,
                });
            }

            return model;
        }

        public async Task SendNotifications()
        {
            var tests = await context.Tests
                .Where(x => !x.NotificationSend)
                .ToListAsync();

            var mail = "youremail";
            var pw = "yourpass";
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(mail, pw),
                EnableSsl = true
            };

            var loop = new List<Test>();

            foreach (var test in tests)
            {
                var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
                var d1 = DateTime.Now.Date.AddDays(-1 * (int)cal.GetDayOfWeek(DateTime.Now));
                var d2 = test.TestDate.Date.AddDays(-1 * (int)cal.GetDayOfWeek(test.TestDate));

                if (d1 == d2)
                {
                    loop.Add(test);
                }
            }

            foreach (var test in loop)
            {
                test.NotificationSend = true;

                var students = await context.Students.Where(x => x.GradeId == test.GradeId).ToListAsync();

                foreach (var user in students)
                {
                    string subject = test.Type == "Класно" ? "Предстояща класна работа" : "Предстояща контролна работа";

                    var u = await context.Users.FindAsync(user.UserId);

                    client.Send(mail, u.Email, subject, $"Здравей {u.FirstName} {u.LastName}! Напомняме ти, че на {test.TestDate.ToString("dd-MM-yyyy")} имаш {test.Type}.");
                }
            }
        }
    }
}
