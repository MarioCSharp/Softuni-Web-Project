using Better_Shkolo.Models.Activity;
using Better_Shkolo.Services.ActivityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class ActivityController : Controller
    {
        private IActivityService activityService;
        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }
        [HttpGet]
        [Authorize(Policy = "DirectorTeacherPolicy")]
        public async Task<IActionResult> Add()
        {
            var model = new ActivityAddModel();

            return View(model);
        }
    }
}
