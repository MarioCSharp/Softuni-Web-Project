using BetterShkolo.Models.Resource;
using BetterShkolo.Services.ResourceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Controllers
{
    public class ResourceController : Controller
    {
        private IResourceService resourceService;
        public ResourceController(IResourceService resourceService)
        {
            this.resourceService = resourceService;
        }
        [HttpGet]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> Add(int lessonId)
        {
            return View(new ResourceModel
            {
                LessonId = lessonId
            });
        }
        [HttpPost]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> Add(List<IFormFile> File, ResourceModel model)
        {
            if (File.Count == 0 && string.IsNullOrEmpty(model.Link))
            {
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await resourceService.AddResource(File, model);

            if (!res) return BadRequest();

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Download(int resourceId)
        {
            var r = await resourceService.GetResource(resourceId);

            if (r.File is null)
            {
                return BadRequest();
            }

            return File(r.File, "application/pdf", $"{r.Name}{r.FileExtension}");
        }
    }
}
