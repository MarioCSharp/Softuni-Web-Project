using Better_Shkolo.Services.TeamService;
using Microsoft.AspNetCore.SignalR;

namespace Better_Shkolo.Hubs
{
    public class ChatHub : Hub
    {
        private ITeamService teamService;
        public ChatHub(ITeamService teamService)
        {
            this.teamService = teamService;
        }
        public async Task SendMessage(string user, string message, string teamId)
        {
            await teamService.SaveMessageAsync(user, message, int.Parse(teamId));
            await Clients.All.SendAsync("RecieveMessage", user, message, teamId);
        }
    }
}
