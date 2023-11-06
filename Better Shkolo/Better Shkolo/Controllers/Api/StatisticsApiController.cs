using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Api;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.StatisticsService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<StatisticsDisplayModel> GetStatistics()
        {
            return await statisticsService.GetStatistics(accountService.GetUserId());
        }

        [HttpGet]
        [Route("GetMark")]
        public async Task<IActionResult> GetMarkById(int id)
        {
            var mark = await statisticsService.GetMarkById(id);

            if (mark is null)
            {
                return NotFound();
            }

            return Ok(mark);
        }

        [HttpGet]
        [Route("GetStatistics")]
        public async Task<IActionResult> GetApplicationStatistics()
        {
            if (!User.IsInRole("Administrator"))
            {
                return BadRequest();
            }

            var statistics = await statisticsService.GetApplicationStatistics();

            return Ok(statistics);
        }

        [HttpGet]
        [Route("GetSchoolStatistics")]
        public async Task<IActionResult> GetSchoolStatistics(int schoolId)
        {
            if (!User.IsInRole("Director") && !User.IsInRole("Administrator"))
            {
                return BadRequest();
            }

            var statistics = await statisticsService.GetSchoolStatistics(schoolId);

            return Ok(statistics);
        }
    }
}
