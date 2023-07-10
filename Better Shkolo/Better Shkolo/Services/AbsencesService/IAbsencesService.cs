using Better_Shkolo.Models.Absence;

namespace Better_Shkolo.Services.AbsenceService
{
    public interface IAbsencesService
    {
        Task<bool> Add(AbsencesAddModel model);
        Task<List<AbsencesesDisplayModel>> GetAbsenceses(string userId);
        Task<List<AbsencesesShowModel>> GetAbsencesesBySubjectId(string userId, int subjectId);
    }
}
