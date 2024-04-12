using BetterShkolo.Data.Models;
using BetterShkolo.Models.Grade;
using BetterShkolo.Models.Teacher;

namespace BetterShkolo.Services.TeacherService
{
    public interface ITeacherService
    {
        Task<List<TeacherDisplayModel>> GetAllTeacherInSchool(int schoolId);
        Task<bool> Create(TeacherCreateModel model);
        Task<bool> DeleteTeacher(int id, int newTeacherId);
        Task<Teacher> GetTeacher(int id);
        Task<Teacher> GetTeacher();
        Task<GradeViewModel> GetGrades();
    }
}
