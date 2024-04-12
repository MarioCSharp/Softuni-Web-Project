using BetterShkolo.Data.Models;
using BetterShkolo.Data;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.School;
using BetterShkolo.Services.AccountService;
using BetterShkolo.Services.StudentService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BetterShkolo.Services.SchoolService
{
    public class SchoolService : ISchoolService
    {
        private ApplicationDbContext context;
        private IAccountService accountService;
        private UserManager<User> userManager;
        private IStudentService studentService;
        public SchoolService(ApplicationDbContext context
                            , UserManager<User> userManager
                            , IAccountService accountService
                            , IStudentService studentService)
        {
            this.context = context;
            this.userManager = userManager;
            this.accountService = accountService;
            this.studentService = studentService;
        }
        public async Task<bool> AddSchool(School school)
        {
            var user = await context.Users.FindAsync(school.DirectorId);

            school.ActiveErasmus = false;

            await context.Schools.AddAsync(school);
            await context.SaveChangesAsync();

            await userManager.AddToRoleAsync(user, "Director");
            await context.SaveChangesAsync();

            await context.Directors.AddAsync(new Director() { SchoolId = school.Id, UserId = user.Id });
            await context.SaveChangesAsync();

            return await context.Schools.ContainsAsync(school);
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

            var d = await context.Directors.FirstOrDefaultAsync(x => x.SchoolId == school.Id);

            context.Directors.Remove(d);
            await context.SaveChangesAsync();

            var parents = await context.Parents.Where(x => x.Student.SchoolId == id).ToArrayAsync();
            var students = await context.Students.Where(x => x.SchoolId == id).ToArrayAsync();
            var teachers = await context.Teachers.Where(x => x.SchoolId == id).ToArrayAsync();

            foreach (var parent in parents)
            {
                await userManager.RemoveFromRoleAsync(await context.Users.FindAsync(parent.UserId), "Parent");
                context.Parents.Remove(parent);
                await context.SaveChangesAsync();
            }

            foreach (var student in students)
            {
                await userManager.RemoveFromRoleAsync(await context.Users.FindAsync(student.UserId), "Student");
            }

            foreach (var teacher in teachers)
            {
                await userManager.RemoveFromRoleAsync(await context.Users.FindAsync(teacher.UserId), "Teacher");
            }

            context.Students.RemoveRange(students);
            context.Marks.RemoveRange(await context.Marks.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Reviews.RemoveRange(await context.Reviews.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Absencess.RemoveRange(await context.Absencess.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Tests.RemoveRange(await context.Tests.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Subjects.RemoveRange(await context.Subjects.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Grades.RemoveRange(await context.Grades.Where(x => x.SchoolId == id).ToArrayAsync());
            context.Teachers.RemoveRange(teachers);

            context.Schools.Remove(school);
            await context.SaveChangesAsync();

            return !await context.Schools.ContainsAsync(school);
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

        public async Task<int> GetSchoolIdByUser()
        {
            var user = await context.Users.FindAsync(accountService.GetUserId());

            var schoolId = -1;

            if (await userManager.IsInRoleAsync(user, "Teacher"))
            {
                var t = await context.Teachers.FirstOrDefaultAsync(x => x.UserId == user.Id);
                var s = await context.Subjects.FirstOrDefaultAsync(x => x.TeacherId == t.Id);

                schoolId = s.SchoolId;
            }
            else if (await userManager.IsInRoleAsync(user, "Director"))
            {
                var d = await context.Directors.FirstOrDefaultAsync(x => x.UserId == user.Id);

                schoolId = d.SchoolId;
            }
            else if (await userManager.IsInRoleAsync(user, "Student"))
            {
                var s = await context.Students.FirstOrDefaultAsync(x => x.UserId == user.Id);

                schoolId = s.SchoolId;
            }
            else if (await userManager.IsInRoleAsync(user, "Parent"))
            {
                var s = await studentService.GetStudentFromParent(user.Id);

                schoolId = s.SchoolId;
            }

            return schoolId;
        }
    }
}
