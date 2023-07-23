using AutoMapper;
using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Test;
using Better_Shkolo.Services.AccountService;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.TestService
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
            test.Id = 0;

            await context.Tests.AddAsync(test);
            await context.SaveChangesAsync();

            return await context.Tests.ContainsAsync(test);
        }

        public async Task<List<TestDisplayModel>> GetTests()
        {
            var userId = accountService.GetUserId();

            var model = new List<TestDisplayModel>();

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
    }
}
