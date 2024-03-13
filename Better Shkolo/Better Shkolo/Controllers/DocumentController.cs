using Better_Shkolo.Models.Document;
using Better_Shkolo.Services.DocumentService;
using Better_Shkolo.Services.SchoolService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
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

            return File(file, "application/pdf", "shablon.pdf");
        }
    }
}
