using Better_Shkolo.Models.Team;
using Better_Shkolo.Services.TeamService;
using BetterShkolo.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class TeamController : Controller
    {
        private ITeamService teamService;
        private IAccountService accountService;
        public TeamController(ITeamService teamService,
                              IAccountService accountService)
        {
            this.teamService = teamService;
            this.accountService = accountService;
        }
        [Authorize(Policy = "DirectorStudentTeacherPolicy")]
        public async Task<IActionResult> Index()
        {
            var model = new List<TeamDisplayModel>();

            if (User.IsInRole("Director"))
            {
                model = await teamService.GetDirectorIndexModel();
            }
            else if (User.IsInRole("Student"))
            {
                model = await teamService.GetStudentIndexModel();
            }
            else if (User.IsInRole("Teacher"))
            {
                model = await teamService.GetTeacherIndexModel();
            }
            else { return BadRequest(); }

            return View(model);
        }
        [Authorize(Policy = "DirectorStudentTeacherPolicy")]
        public async Task<IActionResult> View(int teamId)
        {
            return View(await teamService.GetTeamDetails(teamId));
        }

        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> Add()
        {
            var model = await teamService.GetModel();

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> Add(TeamAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await teamService.AddAsync(model);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }
        [Authorize(Policy = "DirectorStudentTeacherPolicy")]
        public async Task<IActionResult> Attend(int teamId)
        {
            var roomId = await teamService.GetRoomId(teamId);

            if (roomId is null)
            {
                return NotFound();
            }

            return RedirectToAction("Room", "Team", new {roomId = roomId});
        }
        public async Task<IActionResult> Room(string roomId)
        {
            ViewBag.roomId = roomId;
            ViewBag.userId = accountService.GetUserId();
            return View();
        }
    }
}
