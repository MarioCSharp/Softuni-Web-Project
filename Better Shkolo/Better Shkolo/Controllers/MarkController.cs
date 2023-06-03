using Better_Shkolo.Models.Mark;
using Better_Shkolo.Services.MarkService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    [Authorize]
    public class MarkController : Controller
    {
        private IMarkService markService;
        public MarkController(IMarkService markService)
        {
            this.markService = markService;
        }
        [HttpGet]
        public async Task<IActionResult> Add(int id, int subjectId)
        {
            var model = new MarkAddModel()
            {
                StudentId = id,
                SubjectId = subjectId
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(MarkAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await markService.Add(model, model.SubjectId);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Student,Parent")]
        public async Task<IActionResult> View()
        {
            var model = await markService.GetMarks();

            return View(model);
        }
    }
}
