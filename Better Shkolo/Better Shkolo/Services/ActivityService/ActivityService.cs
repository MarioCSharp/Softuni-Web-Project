using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Activity;
using Better_Shkolo.Services.AccountService;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.ActivityService
{
    public class ActivityService : IActivityService
    {
        private ApplicationDbContext context;
        private IAccountService accountService;
        public ActivityService(ApplicationDbContext context,
                               IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
        }
        public async Task<bool> AddAsync(ActivityAddModel model)
        {
            var act = new Activity()
            {
                Name = model.Name,
                Date = model.Date,
                Presence = model.Presence,
                SchoolId = model.SchoolId,
                AddedById = accountService.GetUserId(),
                TimeZone = model.Time,
                Description = model.Description,
                Location = model.Location,
            };

            await context.Activities.AddAsync(act);
            await context.SaveChangesAsync();

            return context.Activities.Contains(act);
        }

        public async Task<ActivityViewModel> GetActivitiesInSchool(int schoolId)
        {
            var activities = await context.Activities
                .Where(x => x.SchoolId == schoolId).OrderByDescending(x => x.Date)
                .Select(x => new ActivityDisplayModel
                {
                    Id = x.Id,
                    ActivityName = x.Name,
                    Description = x.Description,
                    Location = x.Location,
                    Time = x.TimeZone,
                    Day = x.Date.Day,
                    Month = x.Date.ToString("MMMM"),
                    WeekDay = x.Date.ToString("dddd")
                })
                .ToListAsync();

            return new ActivityViewModel()
            {
                Activities = activities
            };
        }
    }
}
