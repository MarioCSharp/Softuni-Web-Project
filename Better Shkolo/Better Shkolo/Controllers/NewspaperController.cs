using BetterShkolo.Models.Newspapper;
using BetterShkolo.Services.NewspapperService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Controllers
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
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await newspapperService.GetPost(id));
        }
    }
}
