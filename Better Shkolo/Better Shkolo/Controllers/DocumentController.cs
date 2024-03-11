using Better_Shkolo.Services.DocumentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class DocumentController : Controller
    {
        private IDocumentService documentService;
        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }
        //[HttpGet]
        //[Authorize(Policy = "DirectorPolicy")]
        //public async Task<IActionResult> Add()
        //{

        //}
    }
}
