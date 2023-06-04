using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Mark;
using Better_Shkolo.Models.Test;
using Better_Shkolo.Services.AccountService;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.TestService
{
    public class TestService : ITestService
    {
        private ApplicationDbContext context;
        private IAccountService accountService;
        public TestService(ApplicationDbContext context, IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
        }
        public async Task<bool> Add(TestAddModel model)
        {
            var count = await context.Tests.CountAsync();
            var subject = await context.Subjects.FindAsync(model.SubjectId);

            if (subject is null)
            {
                return false;
            }

            var test = new Test()
            {
                AddedOn = DateTime.Now,
                TestDate = model.TestDate,
                SubjectId = subject.Id,
                TeacherId = subject.TeacherId,
                GradeId = subject.GradeId,
                SchoolId = subject.SchoolId,
            };

            await context.Tests.AddAsync(test);
            await context.SaveChangesAsync();

            return count + 1 == await context.Tests.CountAsync();
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
