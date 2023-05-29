using Better_Shkolo.Models.Api;

namespace Better_Shkolo.Services.StatisticsService
{
    public interface IStatisticsService
    {
        Task<StatisticsDisplayModel> GetStatistics(string userId);
    }
}
