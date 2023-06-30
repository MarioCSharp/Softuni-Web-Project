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

            var count = await context.Schools.CountAsync();

            var directorUser = await context.Users.FindAsync(school.DirectorId);

            await userManager.RemoveFromRoleAsync(directorUser, "Director");

            var d = await context.Directors.FirstOrDefaultAsync(x => x.UserId == school.DirectorId);

            context.Directors.Remove(d);
            await context.SaveChangesAsync();
            
            var parents = await context.Parents.Where(x => x.Student.SchoolId == id).ToArrayAsync();
            var students = await context.Students.Where(x => x.SchoolId == id).ToArrayAsync();
            var teachers = await context.Teachers.Where(x => x.SchoolId == id).ToArrayAsync();

            foreach (var parent in parents)
            {
                await userManager.RemoveFromRoleAsync(await context.Users.FindAsync(parent.UserId), "Parent");
            }

            foreach (var student in students)
            {
                await userManager.RemoveFromRoleAsync(await context.Users.FindAsync(student.UserId), "Student");
            }

            foreach (var teacher in teachers)
            {
                await userManager.RemoveFromRoleAsync(await context.Users.FindAsync(teacher.UserId), "Teacher");
            }

            context.Absencess.RemoveRange(await context.Absencess.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Grades.RemoveRange(await context.Grades.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Marks.RemoveRange(await context.Marks.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Parents.RemoveRange(parents);
            context.Reviews.RemoveRange(await context.Reviews.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Students.RemoveRange(students);
            context.Subjects.RemoveRange(await context.Subjects.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Teachers.RemoveRange(teachers);
            context.Tests.RemoveRange(await context.Tests.Where(x => x.SchoolId == id).ToArrayAsync());

            context.Schools.Remove(school);
            await context.SaveChangesAsync();

            return count - 1 == await context.Schools.CountAsync();
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
