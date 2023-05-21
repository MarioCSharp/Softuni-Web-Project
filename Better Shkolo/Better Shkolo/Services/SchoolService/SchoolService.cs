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

            await context.Schools.AddAsync(school);
            await context.SaveChangesAsync();

            await userManager.AddToRoleAsync(await context.Users.FindAsync(school.DirectorId), "Director");
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
