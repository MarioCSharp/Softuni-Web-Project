using Better_Shkolo.Models.Api;
using Better_Shkolo.Models.Application;
using Better_Shkolo.Models.Mark;

namespace Better_Shkolo.Services.StatisticsService
{
    public interface IStatisticsService
    {
        Task<StatisticsDisplayModel> GetStatistics(string userId);
        Task<MarkInformationModel> GetMarkById(int id);
        Task<ApplicationStatisticsModel> GetApplicationStatistics();
    }
}
