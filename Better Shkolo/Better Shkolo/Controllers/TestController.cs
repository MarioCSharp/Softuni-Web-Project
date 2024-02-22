using Better_Shkolo.Models.Test;
using Better_Shkolo.Services.TestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Better_Shkolo.Controllers
{
    public class TestController : Controller
    {
        private ITestService testService;
        public TestController(ITestService testService)
        {
            this.testService = testService;
        }
        [HttpGet]
        [Authorize(Policy = "TeacherPolicy")]
        public IActionResult Add(int id)
        {
            var model = new TestAddModel()
            {
                SubjectId = id
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> Add(TestAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await testService.Add(model);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction(nameof(AlreadyExists));
        }
        [Authorize]
        public async Task<IActionResult> AlreadyExists()
        {
            return await View();
        }
        [HttpGet]
        [Authorize(Policy = "StudentParentPolicy")]
        public async Task<IActionResult> View()
        {
            var model = await testService.GetTests();

            return View(model);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Schedule(int gradeId)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            var week = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            var gradeSchedule = await testService.GetSchedule(gradeId, week);

            return View(gradeSchedule);
        }
    }
}
