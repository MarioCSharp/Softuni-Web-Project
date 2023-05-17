using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Teacher;

namespace Better_Shkolo.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private ApplicationDbContext context;
        public TeacherService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool Create(TeacherCreateModel model)
        {
            var countNow = context.Teachers.Count();

            var teacher = new Teacher()
            {
                SchoolId = model.SchoolId,
                UserId = model.UserId
            };

            context.Teachers.Add(teacher);
            context.SaveChanges();

            return countNow + 1 == context.Teachers.Count();
        }

        public bool DeleteTeacher(int id)
        {
            var count = context.Teachers.Count();
            var teacher = context.Teachers.FirstOrDefault(x => x.Id == id);

            if (teacher is null)
            {
                return false;
            }

            context.Teachers.Remove(teacher);
            context.SaveChanges();

            return count - 1 == context.Teachers.Count();
        }

        public List<TeacherDisplayModel> GetAllTeacherInSchool(int schoolId, string userId)
        {
            return context.Teachers.Where(x => x.SchoolId == schoolId)
                .Select(x => new TeacherDisplayModel
                {
                    Id = x.Id,
                    FirstName = context.Users.FirstOrDefault(x => x.Id == userId).FirstName,
                    LastName = context.Users.FirstOrDefault(x => x.Id == userId).LastName,
                    Email = context.Users.FirstOrDefault(x => x.Id == userId).Email,
                    SchoolId = schoolId
                }).ToList();
        }
    }
}
