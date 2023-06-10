using Better_Shkolo.Models.Test;
using Better_Shkolo.Services.TestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize(Policy = "CanAddTests")]
        public IActionResult Add(int id)
        {
            var model = new TestAddModel()
            {
                SubjectId = id
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "CanAddTests")]
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

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "CanViewTests")]
        public async Task<IActionResult> View()
        {
            var model = await testService.GetTests();

            return View(model);
        }
    }
}
