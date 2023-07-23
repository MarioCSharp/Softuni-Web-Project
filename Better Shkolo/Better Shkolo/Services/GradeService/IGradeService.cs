using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.Student;

namespace Better_Shkolo.Services.GradeService
{
    public interface IGradeService
    {
        Task<bool> Create(GradeCreateModel model);
        Task<bool> DeleteGrade(int id);
        Task<Grade> GetGrade(int id);
        Task<List<GradeDisplayModel>> GetGradesBySchoolId(int schoolId);
        Task<Grade> GetGradeByTeacherId(int teacherId);
        Task<List<StudentDisplayModel>> GetStudentsInGrade(string userId);
        Task<GradeStatisticsModel> GetGradeStatistics(Grade grade);
    }
}
