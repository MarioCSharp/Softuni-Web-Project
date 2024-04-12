using BetterShkolo.Models.Student;

namespace BetterShkolo.Services.ErasmusService
{
    public interface IErasmusService
    {
        List<StudentDisplayModel> GetAligibleStudents(int schoolId);
        Task Activate(int schoolId);
        Task Deactivate(int schoolId);
        Task<bool> Apply();
    }
}
