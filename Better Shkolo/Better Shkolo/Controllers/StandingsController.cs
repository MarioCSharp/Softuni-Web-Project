using BetterShkolo.Models.Standings;
using BetterShkolo.Services.StandingsService;
using BetterShkolo.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BetterShkolo.Controllers
{
    public class StandingsController : Controller
    {
        private IStudentService studentService;
        private IStandingsService standingsService;

        public StandingsController(IStudentService studentService,
                                   IStandingsService standingsService)
        {
            this.studentService = studentService;
            this.standingsService = standingsService;
        }

        [HttpGet]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> View(string searchTerm)
        {
            var stundentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var student = await studentService.GetStudent(stundentUserId);

            var model = await standingsService.GetStandings(student, searchTerm);

            return View(model);
        }
    }
}
