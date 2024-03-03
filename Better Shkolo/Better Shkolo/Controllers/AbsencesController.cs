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
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> Add(int id, int subjectId)
        {
            var model = await studentService.GetStudentModel(id);

            model.SubjectId = subjectId;

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "TeacherPolicy")]
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
        [Authorize(Policy = "StudentParentPolicy")]
        public async Task<IActionResult> View()
        {
            var model = await absencesService.GetAbsenceses(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Display(int subjectId, string userId)
        {
            var absencesInSubject = await absencesService.GetAbsencesesBySubjectId(userId, subjectId);

            if (absencesInSubject is null)
            {
                return BadRequest();
            }

            return View(absencesInSubject);
        }
        [HttpGet]
        [Authorize(Policy = "AdministratorDirectorTeacherPolicy")]
        public async Task<IActionResult> Excuse(int id)
        {
            var absences = await absencesService.GetAbsences(id);

            if (absences is null)
            {
                return BadRequest();
            }

            absencesService.Excuse(absences);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> ById(int studentId)
        {
            var student = await studentService.GetStudent(studentId);

            if (student is null)
            {
                return BadRequest();
            }

            var studentAbsenceses = await absencesService.GetAllStudentAbsenceses(studentId);

            return View(studentAbsenceses);
        }
    }
}
