using BetterShkolo.Data;

namespace Better_Shkolo.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private ApplicationDbContext context;
        public TeamService(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
