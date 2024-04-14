using AutoMapper;
using BetterShkolo.Data.Models;
using BetterShkolo.Data;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.Mark;
using BetterShkolo.Services.AccountService;
using Microsoft.EntityFrameworkCore;

namespace BetterShkolo.Services.MarkService
{
    public class MarkService : IMarkService
    {
        private ApplicationDbContext context;
        private IMapper mapper;
        private IAccountService accountService;
        public MarkService(ApplicationDbContext context, IMapper mapper,
                           IAccountService accountService)
        {
            this.context = context;
            this.mapper = mapper;
            this.accountService = accountService;
        }
        public async Task<bool> Add(MarkAddModel model, int subjectId, string userId)
        {
            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.UserId == userId);
            var subject = await context.Subjects.FindAsync(subjectId);
            var student = await context.Students.FindAsync(model.StudentId);

            if (teacher == null || subject == null || student == null || model.Value < 2 || model.Value > 6)
            {
                return false;
            }

            var teacherId = teacher.Id;

            var schoolId = teacher.SchoolId;

            var mark = mapper.Map<Mark>(model);
            mark.AddedOn = DateTime.Now;
            mark.TeacherId = teacherId;
            mark.SchoolId = schoolId;
            mark.Term = model.Term;
            mark.Type = model.Type;

            await context.Marks.AddAsync(mark);
            await context.SaveChangesAsync();

            return await context.Marks.ContainsAsync(mark);
        }

        public async Task<bool> AddTermMark(TermMarkAddModel model)
        {
            var student = await context.Students.FindAsync(model.StudentId);
            var subject = await context.Subjects.FindAsync(model.SubjectId);
            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.UserId == accountService.GetUserId());

            if (student is null || subject is null || teacher is null) return false;

            var t = new TermMark()
            {
                Value = model.Value,
                StudentId = student.Id,
                SubjectId = subject.Id,
                Term = model.Term,
                TeacherId = teacher.Id,
            };

            await context.TermMarks.AddAsync(t);
            await context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> AddYearMark(YearMarkAddModel model)
        {
            var student = await context.Students.FindAsync(model.StudentId);
            var subject = await context.Subjects.FindAsync(model.SubjectId);
            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.UserId == accountService.GetUserId());

            if (student is null || subject is null || teacher is null) return false;

            var t = new YearMark()
            {
                Value = model.Value,
                StudentId = student.Id,
                SubjectId = subject.Id,
                TeacherId = teacher.Id,
            };

            await context.YearMarks.AddAsync(t);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<double> GetAverageMarks(string userId)
        {
            var marks = await context.Marks.Where(x => x.Student.UserId == userId).ToListAsync();

            if (marks.Count == 0)
            {
                return 0.0;
            }

            return marks.Average(x => x.Value);
        }

        public async Task<List<MarkDisplayModel>> GetMarks(string userId)
        {
            var model = new List<MarkDisplayModel>();

            var student = await context.Students.FirstOrDefaultAsync(x => x.UserId == userId);

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

            var marks = await context.Marks.Where(x => x.StudentId == studentId).ToListAsync();

            foreach (var mark in marks)
            {
                var subjectId = mark.SubjectId;

                if (!model.Any(x => x.SubjectId == subjectId))
                {
                    var subject = await context.Subjects.FindAsync(subjectId);

                    model.Add(new MarkDisplayModel()
                    {
                        SubjectId = subjectId,
                        SubjectName = subject.Name,
                        MarkId = mark.Id,
                        Marks = new List<MarkViewModel>()
                    });
                }

                var teacher = await context.Teachers.FindAsync(mark.TeacherId);
                var teacherUser = await context.Users.FindAsync(teacher.UserId);

                model.FirstOrDefault(x => x.SubjectId == subjectId).Marks.Add(new MarkViewModel()
                {
                    Id = mark.Id,
                    Value = mark.Value,
                    TeacherId = teacher.Id,
                    TeacherName = teacherUser.FirstName + " " + teacherUser.LastName,
                });
            }

            return model;
        }
    }
}
