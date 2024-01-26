using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Api;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.StatisticsService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private IStatisticsService statisticsService;
        private IAccountService accountService;
        public StatisticsApiController(IStatisticsService statisticsService,
                                       IAccountService accountService,
                                       SignInManager<User> signInManager)
        {
            this.statisticsService = statisticsService;
            this.accountService = accountService;
        }
        [HttpGet]
        [Route("GetStudentStatistics")]
        public StatisticsDisplayModel GetStatistics()
        {
            return statisticsService.GetStatistics(accountService.GetUserId());
        }

        [HttpGet]
        [Route("GetMark")]
        public IActionResult GetMarkById(int id)
        {
            var mark = statisticsService.GetMarkById(id);

            if (mark is null)
            {
                return NotFound();
            }

            return Ok(mark);
        }

        [HttpGet]
        [Route("GetStatistics")]
        public IActionResult GetApplicationStatistics()
        {
            if (!User.IsInRole("Administrator"))
            {
                return BadRequest();
            }

            var statistics = statisticsService.GetApplicationStatistics();

            return Ok(statistics);
        }

        [HttpGet]
        [Route("GetSchoolStatistics")]
        public IActionResult GetSchoolStatistics(int schoolId)
        {
            if (!User.IsInRole("Director") && !User.IsInRole("Administrator"))
            {
                return BadRequest();
            }

            var statistics = statisticsService.GetSchoolStatistics(schoolId);

            return Ok(statistics);
        }
    }
}
