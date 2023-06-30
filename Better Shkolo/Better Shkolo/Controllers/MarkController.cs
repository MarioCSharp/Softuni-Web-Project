using Better_Shkolo.Models.Mark;
using Better_Shkolo.Services.MarkService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Better_Shkolo.Controllers
{
    public class MarkController : Controller
    {
        private IMarkService markService;
        public MarkController(IMarkService markService)
        {
            this.markService = markService;
        }
        [HttpGet]
        [Authorize(Policy = "CanAddMarks")]
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
        [Authorize(Policy = "CanAddMarks")]
        public async Task<IActionResult> Add(MarkAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await markService.Add(model, model.SubjectId, User.FindFirstValue(ClaimTypes.Name));

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "CanViewMarks")]
        public async Task<IActionResult> View()
        {
            var model = await markService.GetMarks(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(model);
        }
    }
}
