using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.Teacher;
using Better_Shkolo.Services.AccountService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private ApplicationDbContext context;
        private UserManager<User> userManager;
        private IAccountService accountService;
        public TeacherService(ApplicationDbContext context,
                              UserManager<User> userManager,
                              IAccountService accountService)
        {
            this.context = context;
            this.userManager = userManager;
            this.accountService = accountService;

        }

        public async Task<bool> Create(TeacherCreateModel model)
        {
            var teacher = new Teacher()
            {
                SchoolId = model.SchoolId,
                UserId = model.UserId
            };

            await context.Teachers.AddAsync(teacher);
            await context.SaveChangesAsync();

            var user = await context.Users.FindAsync(model.UserId);

            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Teacher");
            }
            else
            {
                return false;
            }

            return await context.Teachers.ContainsAsync(teacher);
        }

        public async Task<bool> DeleteTeacher(int id, int newTeacherId = 0)
        {
            var teacher = await context.Teachers.FindAsync(id);
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == teacher.UserId);

            if (teacher is null)
            {
                return false;
            }

            await userManager
                .RemoveFromRoleAsync(user, "Teacher");

            if (newTeacherId != 0)
            {
                var students = await context.Students.Where(x => x.GradeTeacherId == id).ToListAsync();

                foreach (var student in students)
                {
                    student.GradeTeacherId = newTeacherId;
                    await context.SaveChangesAsync();
                }
            }

            context.Teachers.Remove(teacher);
            await context.SaveChangesAsync();

            return !await context.Teachers.ContainsAsync(teacher);
        }
        public async Task<List<TeacherDisplayModel>> GetAllTeacherInSchool(int schoolId)
        {
            var result = await context.Teachers.Where(x => x.SchoolId == schoolId)
                .Select(x => new TeacherDisplayModel
                {
                    Id = x.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email,
                    SchoolId = schoolId
                }).ToListAsync();

            return result;
        }

        public async Task<GradeViewModel> GetGrades()
        {
            var uId = accountService.GetUserId();

            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.UserId == uId);

            if (teacher is null)
            {
                return new GradeViewModel();
            }

            var model = new GradeViewModel()
            {
                Grades = await context.Subjects
                .Where(x => x.TeacherId == teacher.Id)
                .Select(x => x.Grade).Select(x => new GradeDisplayModel()
                {
                    Id = x.Id,
                    GradeName = x.GradeName
                }).Distinct().ToListAsync()
            };

            return model;
        }

        public async Task<Teacher> GetTeacher(int id)
        {
            return await context.Teachers.FindAsync(id);
        }

        public async Task<Teacher> GetTeacher()
        {
            return await context.Teachers.FirstOrDefaultAsync(x => x.UserId == accountService.GetUserId());
        }
    }
}
