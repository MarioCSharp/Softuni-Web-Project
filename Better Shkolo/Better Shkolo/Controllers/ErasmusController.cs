using Better_Shkolo.Models.Erasmus;
using BetterShkolo.Models.Erasmus;
using BetterShkolo.Services.AccountService;
using BetterShkolo.Services.ErasmusService;
using BetterShkolo.Services.MarkService;
using BetterShkolo.Services.SchoolService;
using BetterShkolo.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace BetterShkolo.Controllers
{
    public class ErasmusController : Controller
    {
        private IErasmusService erasmusService;
        private ISchoolService schoolService;
        private IAccountService accountService;
        private IStudentService studentService;
        private IMarkService markService;
        public ErasmusController(IErasmusService erasmusService,
                                 ISchoolService schoolService,
                                 IAccountService accountService,
                                 IStudentService studentService,
                                 IMarkService markService)
        {
            this.erasmusService = erasmusService;
            this.schoolService = schoolService;
            this.accountService = accountService;
            this.studentService = studentService;
            this.markService = markService;
        }

        [HttpGet]
        [Authorize(Policy = "DirectorStudentParentTeacherPolicy")]
        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var uSchoolId = await schoolService.GetSchoolIdByUser();
            var school = await schoolService.GetSchool(uSchoolId);

            var students = erasmusService.GetAligibleStudents(uSchoolId);
            var totalStudents = students.Count;

            int studentsToSkip = (currentPage - 1) * 8;

            var model = new ErasmusIndexModel()
            {
                SchoolId = uSchoolId,
                SchoolName = school.Name,
                Active = school.ActiveErasmus,
                CurrentPage = currentPage,
                StudentsPerPage = 8,
                AligibleStudents = erasmusService.GetAligibleStudents(uSchoolId),
                Documents = await erasmusService.GetSchoolDocuments(uSchoolId),
                TotalPages = (int)Math.Ceiling((double)totalStudents / 8)
            };

            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> Activate(int schoolId)
        {
            await erasmusService.Activate(schoolId);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> Deactivate(int schoolId)
        {
            await erasmusService.Deactivate(schoolId);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> Apply(int schoolId)
        {
            var schoolIsActive = await erasmusService.SchoolIsActive(schoolId);

            if (!schoolIsActive)
            {
                return BadRequest();
            }

            var uId = accountService.GetUserId();

            var marks = await markService.GetAverageMarks(uId);

            if (marks < 4.50)
            {
                return BadRequest();
            }

            return View();
        }
        [HttpPost]
        [Authorize(Policy = "StudentPolicy")]
        public async Task<IActionResult> Apply(ErasmusApplyModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await erasmusService.Apply(model);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> AddDocument()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "DirectorPolicy")]
        public async Task<IActionResult> AddDocument(ErasmusDocumentAddModel model, IFormFile File)
        {
            if (!ModelState.IsValid || File is null)
            {
                return View(model);
            }

            var result = await erasmusService.AddDocument(model, File);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Download(int documentId)
        {
            var document = await erasmusService.GetDoc(documentId);

            return File(document.File, "application/pdf", $"{document.Name}{document.FileExtension}");
        }
    }
}
