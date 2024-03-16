using Better_Shkolo.Models.Api;
using Better_Shkolo.Models.Application;
using Better_Shkolo.Models.Mark;
using Better_Shkolo.Models.Teacher;

namespace Better_Shkolo.Services.StatisticsService
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
