using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Test;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.TestService
{
    public class TestService : ITestService
    {
        private ApplicationDbContext context;
        public TestService(ApplicationDbContext context)
        {
            this.context = context;
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
    }
}
