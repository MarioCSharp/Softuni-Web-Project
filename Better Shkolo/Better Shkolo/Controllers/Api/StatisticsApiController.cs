using Better_Shkolo.Models.Api;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.StatisticsService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Better_Shkolo.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private IStatisticsService statisticsService;
        private IAccountService accountService;
        private IMemoryCache memoryCache;
        public StatisticsApiController(IStatisticsService statisticsService,
                                       IAccountService accountService,
                                       IMemoryCache memoryCache)
        {
            this.statisticsService = statisticsService;
            this.accountService = accountService;
            this.memoryCache = memoryCache;
        }
        [HttpGet]
        public async Task<StatisticsDisplayModel> GetStatistics()
        {
            return await statisticsService.GetStatistics(accountService.GetUserId());
        }
    }
}
