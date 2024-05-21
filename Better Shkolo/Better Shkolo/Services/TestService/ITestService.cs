using BetterShkolo.Models.Test;

namespace BetterShkolo.Services.TestService
{
    public interface ITestService
    {
        Task<bool> Add(TestAddModel model);
        Task<List<TestDisplayModel>> GetTests();
        Task<TestViewScheduleModel> GetSchedule(int gradeId, int week);
        Task SendNotifications();
    }
}
