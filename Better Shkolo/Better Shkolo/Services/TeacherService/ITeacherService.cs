using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Teacher;

namespace Better_Shkolo.Services.TeacherService
{
    public interface ITeacherService
    {
        Task<List<TeacherDisplayModel>> GetAllTeacherInSchool(int schoolId);
        Task<bool> Create(TeacherCreateModel model);
        Task<bool> DeleteTeacher(int id);
        Task<Teacher> GetTeacher(int id);
        Task<Teacher> GetTeacher();
    }
}
