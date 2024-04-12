using BetterShkolo.Models.School;

namespace BetterShkolo.Services.DirectorService
{
    public interface IDirectorService
    {
        Task<SchoolViewModel> GetSchoolByUser();
    }
}
