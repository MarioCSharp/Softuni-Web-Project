using Better_Shkolo.Data;
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
