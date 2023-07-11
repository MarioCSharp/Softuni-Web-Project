using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Absence;

namespace Better_Shkolo.Services.AbsenceService
{
    public interface IAbsencesService
    {
        Task<bool> Add(AbsencesAddModel model);
        Task<List<AbsencesesDisplayModel>> GetAbsenceses(string userId);
        Task<List<AbsencesesShowModel>> GetAbsencesesBySubjectId(string userId, int subjectId);
        Task<List<AbsencesesShowModel>> GetAllStudentAbsenceses(int studentId);
        Task<Absences> GetAbsences(int id);
        void Excuse(Absences absences);
    }
}
