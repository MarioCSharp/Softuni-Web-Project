using BetterShkolo.Models.Api;
using BetterShkolo.Models.Application;
using BetterShkolo.Models.Mark;
using BetterShkolo.Models.Teacher;

namespace BetterShkolo.Services.StatisticsService
{
    public interface IStatisticsService
    {
        StatisticsDisplayModel GetStatistics(string userId);
        MarkInformationModel GetMarkById(int id);
        ApplicationStatisticsModel GetApplicationStatistics();
        ApplicationStatisticsModel GetSchoolStatistics(int schoolId);
        Task<TeacherHomeModel> GetTeacherHomeModel();
    }
}
