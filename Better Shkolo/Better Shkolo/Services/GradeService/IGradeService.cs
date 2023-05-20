using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Grade;

namespace Better_Shkolo.Services.GradeService
{
    public interface IGradeService
    {
        Task<bool> Create(GradeCreateModel model);
        bool DeleteGrade(int id);
        Task<Grade> GetGrade(int id);
        List<GradeDisplayModel> GetGradesBySchoolId(int schoolId);
        Task<Grade> GetGradeByTeacherId(int teacherId);
    }
}
