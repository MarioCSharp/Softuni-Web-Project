using Better_Shkolo.Models.Diploma;
using Better_Shkolo.Services.DiplomaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class DiplomaController : Controller
    {
        private IDiplomaService diplomaService;
        public DiplomaController(IDiplomaService diplomaService)
        {
            this.diplomaService = diplomaService;   
        }
        [HttpGet]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> Index(string docType)
        {
            if (string.IsNullOrWhiteSpace(docType))
            {
                return View(await diplomaService.GetSchoolDiplomas());
            }

            return View(await diplomaService.GetSchoolDiplomas(docType));
        }

        [HttpGet]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> Add(DiplomaAddModel model, IFormFile File)
        {
            if (!ModelState.IsValid || File.Length == 0)
            {
                return View(model);
            }

            var result = await diplomaService.AddDiploma(model, File);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
