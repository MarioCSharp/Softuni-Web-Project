using Better_Shkolo.Data;
using Better_Shkolo.Models.School;
using Better_Shkolo.Services.AccountService;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.DirectorService
{
    public class DirectorService : IDirectorService
    {
        private ApplicationDbContext context;
        private IAccountService accountService;

        public DirectorService(ApplicationDbContext context,
                               IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
        }

        public async Task<SchoolViewModel> GetSchoolByUser()
        {
            var userId = accountService.GetUserId();

            var director = await context.Directors.FirstOrDefaultAsync(x => x.UserId == userId);

            var school = await context.Schools.FindAsync(director?.SchoolId);

            var model = new SchoolViewModel()
            {
                Id = school.Id,
                Name = school.Name,
                City = school.City,
            };

            return model;
        }
    }
}
