using Better_Shkolo.Models.Activity;

namespace Better_Shkolo.Services.ActivityService
{
    public interface IActivityService
    {
        Task<bool> AddAsync(ActivityAddModel model);
        Task<ActivityViewModel> GetActivitiesInSchool(int schoolId);
    }
}
