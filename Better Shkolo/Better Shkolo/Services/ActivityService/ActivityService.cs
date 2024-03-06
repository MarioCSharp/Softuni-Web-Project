using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Activity;
using Better_Shkolo.Services.AccountService;

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
                AddedById = accountService.GetUserId()
            };

            await context.Activities.AddAsync(act);
            await context.SaveChangesAsync();

            return context.Activities.Contains(act);
        }
    }
}
