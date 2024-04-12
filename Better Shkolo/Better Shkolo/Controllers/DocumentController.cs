using BetterShkolo.Models.Document;
using BetterShkolo.Services.DocumentService;
using BetterShkolo.Services.SchoolService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Controllers
{
    public class DocumentController : Controller
    {
        private IDocumentService documentService;
        private ISchoolService schoolService;
        public DocumentController(IDocumentService documentService,
                                  ISchoolService schoolService)
        {
            this.documentService = documentService;
            this.schoolService = schoolService;
        }
        [HttpGet]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> Add(List<IFormFile> File, DocumentAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await documentService.AddAsync(File, model);

            if (result is false)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Policy = "DirectorTeacherPolicy")]
        public async Task<IActionResult> Index()
        {
            var filesInSchool = await documentService.GetFilesInSchool(await schoolService.GetSchoolIdByUser());

            return View(filesInSchool);
        }
        [HttpGet]
        [Authorize(Policy = "DirectorTeacherPolicy")]
        public async Task<IActionResult> Download(int documentId)
        {
            var file = await documentService.GetFile(documentId);
            var extension = await documentService.GetExtension(documentId);

            return File(file, "application/pdf", $"shablon{extension}");
        }
        [HttpGet]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> Delete(int documentId)
        {
            var res = await documentService.Delete(documentId);

            if (res is false)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
