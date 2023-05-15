using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.School;
using Microsoft.AspNetCore.Identity;

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

            context.Schools.Add(school);
            context.SaveChanges();

            await userManager.AddToRoleAsync(context.Users.First(x => x.Id == school.DirectorId), "Director");
            context.SaveChanges();

            return countBefore + 1 == context.Schools.Count();
        }

        public bool DeleteSchool(int id)
        {
            var school = context.Schools.Find(id);

            if (school == null)
            {
                return false;
            }

            context.Schools.Remove(school);
            context.SaveChanges();

            return true;
        }

        public List<SchoolViewModel> GetAllSchools()
        {
            return context.Schools.Select(x => new SchoolViewModel
            {
                Id = x.Id,
                Name = x.Name,
                City = x.City,
            }).ToList();
        }

        public School GetSchool(int id)
        {
            var school = context.Schools.FirstOrDefault(x => x.Id == id);

            return school;
        }
    }
}
