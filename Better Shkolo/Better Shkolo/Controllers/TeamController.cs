using Better_Shkolo.Services.TeamService;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class TeamController : Controller
    {
        private ITeamService teamService;
        public TeamController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Room", "Team", new {roomId = Guid.NewGuid()});
        }
        public async Task<IActionResult> Room(string roomId)
        {
            ViewBag.roomId = roomId;
            return View();
        }
    }
}
