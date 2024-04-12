using BetterShkolo.Models.Activity;

namespace BetterShkolo.Services.ActivityService
{
    public interface IActivityService
    {
        Task<bool> AddAsync(ActivityAddModel model);
        Task<ActivityViewModel> GetActivitiesInSchool(int schoolId);
    }
}
