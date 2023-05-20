using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Teacher;
using Microsoft.AspNetCore.Identity;

namespace Better_Shkolo.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private ApplicationDbContext context;
        private UserManager<User> userManager;
        public TeacherService(ApplicationDbContext context,
                              UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<bool> Create(TeacherCreateModel model)
        {
            var countNow = context.Teachers.Count();

            var teacher = new Teacher()
            {
                SchoolId = model.SchoolId,
                UserId = model.UserId
            };

            await context.Teachers.AddAsync(teacher);
            await context.SaveChangesAsync();

            var user = context.Users.FirstOrDefault(x => x.Id == model.UserId);

            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Teacher");
            }
            else
            {
                return false;
            }

            return countNow + 1 == context.Teachers.Count();
        }

        public async Task<bool> DeleteTeacher(int id)
        {
            var count = context.Teachers.Count();
            var teacher = context.Teachers.FirstOrDefault(x => x.Id == id);

            if (teacher is null)
            {
                return false;
            }

            context.Teachers.Remove(teacher);
            await context.SaveChangesAsync();

            await userManager
                .RemoveFromRoleAsync(context.Users.FirstOrDefault(x => x.Id == teacher.UserId), "Teacher");

            return count - 1 == context.Teachers.Count();
        }

        public List<TeacherDisplayModel> GetAllTeacherInSchool(int schoolId)
        {
            var result = context.Teachers.Where(x => x.SchoolId == schoolId)
                .Select(x => new TeacherDisplayModel
                {
                    Id = x.Id,
                    FirstName = context.Users.FirstOrDefault(y => y.Id == x.UserId).FirstName,
                    LastName = context.Users.FirstOrDefault(y => y.Id == x.UserId).LastName,
                    Email = context.Users.FirstOrDefault(y => y.Id == x.UserId).Email,
                    SchoolId = schoolId
                }).ToList();

            return result;
        }

        public async Task<Teacher> GetTeacher(int id)
        {
            return await context.Teachers.FindAsync(id);
        }
    }
}
