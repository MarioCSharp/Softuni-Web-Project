using Better_Shkolo.Models.Newspapper;
using Better_Shkolo.Services.NewspapperService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class NewspaperController : Controller
    {
        private INewspapperService newspapperService;
        public NewspaperController(INewspapperService newspapperService)
        {
            this.newspapperService = newspapperService;
        }
        [HttpGet]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> Post()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> Post(List<IFormFile> Image, NewspapperAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await newspapperService.PostAsync(Image, model);

            if (!res) return BadRequest();

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await newspapperService.GetNews());
        }
    }
}
