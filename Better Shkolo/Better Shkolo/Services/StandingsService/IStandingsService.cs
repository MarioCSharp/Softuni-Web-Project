using BetterShkolo.Models.Account;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.Standings;

namespace BetterShkolo.Services.StandingsService
{
    public interface IStandingsService
    {
        Task<StandingsDisplayModel> GetStandings(Student student, string searchTerm);
        Task<StandingsDisplayModel> GetPlaces(Student student);
    }
}
