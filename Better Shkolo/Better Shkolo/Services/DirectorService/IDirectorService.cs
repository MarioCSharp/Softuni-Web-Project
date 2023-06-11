using Better_Shkolo.Models.School;

namespace Better_Shkolo.Services.DirectorService
{
    public interface IDirectorService
    {
        Task<SchoolViewModel> GetSchoolByUser();
    }
}
