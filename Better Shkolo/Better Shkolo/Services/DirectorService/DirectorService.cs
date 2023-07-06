using AutoMapper;
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
        private IMapper mapper;
        public DirectorService(ApplicationDbContext context,
                               IAccountService accountService,
                               IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.accountService = accountService;
        }

        public async Task<SchoolViewModel> GetSchoolByUser()
        {
            var userId = accountService.GetUserId();

            var director = await context.Directors.FirstOrDefaultAsync(x => x.UserId == userId);

            var school = await context.Schools.FindAsync(director?.SchoolId);

            var model = mapper.Map<SchoolViewModel>(school);

            return model;
        }
    }
}
