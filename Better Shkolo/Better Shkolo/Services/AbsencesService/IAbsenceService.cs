using Better_Shkolo.Models.Absence;

namespace Better_Shkolo.Services.AbsenceService
{
    public interface IAbsenceService
    {
        Task<bool> Add(AbsencesAddModel model, int id);
    }
}
