using BetterShkolo.Data;
using BetterShkolo.Models.Student;
using Microsoft.EntityFrameworkCore;

namespace BetterShkolo.Services.ErasmusService
{
    public class ErasmusService : IErasmusService
    {
        private ApplicationDbContext context;
        public ErasmusService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Activate(int schoolId)
        {
            var school = await context.Schools.FindAsync(schoolId);

            school.ActiveErasmus = true;
            await context.SaveChangesAsync();
        }

        public Task<bool> Apply()
        {
            throw new NotImplementedException();
        }

        public async Task Deactivate(int schoolId)
        {
            var school = await context.Schools.FindAsync(schoolId);

            school.ActiveErasmus = false;
            await context.SaveChangesAsync();
        }

        public List<StudentDisplayModel> GetAligibleStudents(int schoolId)
        {
            var allStudents = context.Students
                .Where(s => s.SchoolId == schoolId && s.Marks.Average(x => x.Value) >= 4.50);

            return allStudents.Select(s => new StudentDisplayModel
            {
                Id = s.Id,
                FirstName = s.User.FirstName,
                LastName = s.User.LastName,
                Email = s.User.Email,
                UserId = s.UserId,
                SchoolId = s.SchoolId,
                SchoolName = s.School.Name
            }).ToList();
        }
    }
}
