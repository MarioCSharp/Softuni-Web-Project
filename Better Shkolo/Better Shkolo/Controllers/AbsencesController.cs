using Better_Shkolo.Models.Absence;
using Better_Shkolo.Services.AbsenceService;
using Better_Shkolo.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Better_Shkolo.Controllers
{
    public class AbsencesController : Controller
    {
        private IStudentService studentService;
        private IAbsencesService absencesService;
        public AbsencesController(IStudentService studentService,
                                 IAbsencesService absenceService)
        {
            this.studentService = studentService;
            this.absencesService = absenceService;
        }
        [HttpGet]
        [Authorize(Policy = "CanAddAbsenceses")]
        public async Task<IActionResult> Add(int id, int subjectId)
        {
            var model = await studentService.GetStudentModel(id);

            model.SubjectId = subjectId;

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "CanAddAbsenceses")]
        public async Task<IActionResult> Add(AbsencesAddModel model)
        {
            var result = await absencesService.Add(model);

            if (!result)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize(Policy = "CanViewAbsencesesForStudent")]
        public async Task<IActionResult> View()
        {
            var model = await absencesService.GetAbsenceses(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "CanViewAbsencesesForStudent")]
        public async Task<IActionResult> Display(int subjectId)
        {
            var absencesInSubject = await absencesService.GetAbsencesesBySubjectId(User.FindFirstValue(ClaimTypes.NameIdentifier), subjectId);

            if (absencesInSubject is null)
            {
                return BadRequest();
            }

            return View(absencesInSubject);
        }
    }
}
