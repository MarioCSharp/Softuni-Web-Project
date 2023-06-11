using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.School;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.SchoolService
{
    public class SchoolService : ISchoolService
    {
        private ApplicationDbContext context;
        private UserManager<User> userManager;
        public SchoolService(ApplicationDbContext context
                            , UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<bool> AddSchool(School school)
        {
            var countBefore = context.Schools.Count();
            var user = await context.Users.FindAsync(school.DirectorId);

            await context.Schools.AddAsync(school);
            await context.SaveChangesAsync();

            await userManager.AddToRoleAsync(user, "Director");
            await context.SaveChangesAsync();

            await context.Directors.AddAsync(new Director() { SchoolId = school.Id, UserId = user.Id});
            await context.SaveChangesAsync();

            return countBefore + 1 == await context.Schools.CountAsync();
        }

        public async Task<bool> DeleteSchool(int id)
        {
            var school = await context.Schools.FindAsync(id);

            if (school == null)
            {
                return false;
            }

            context.Absencess.RemoveRange(await context.Absencess.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Grades.RemoveRange(await context.Grades.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Marks.RemoveRange(await context.Marks.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Parents.RemoveRange(await context.Parents.Where(x => x.Student.Id == id).ToArrayAsync());
            context.Reviews.RemoveRange(await context.Reviews.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Students.RemoveRange(await context.Students.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Subjects.RemoveRange(await context.Subjects.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Teachers.RemoveRange(await context.Teachers.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Tests.RemoveRange(await context.Tests.Where(x => x.SchoolId == id).ToArrayAsync());

            context.Schools.Remove(school);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<SchoolViewModel>> GetAllSchools()
        {
            return await context.Schools.Select(x => new SchoolViewModel 
            { 
                Id = x.Id,
                Name = x.Name,
                City = x.City 
            }).ToListAsync();
        }

        public async Task<School> GetSchool(int id)
        {
            var school = await context.Schools.FindAsync(id);

            return school;
        }
    }
}
