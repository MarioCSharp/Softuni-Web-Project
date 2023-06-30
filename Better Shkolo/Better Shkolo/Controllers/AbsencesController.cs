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
        private IAbsencesService absenceService;
        public AbsencesController(IStudentService studentService,
                                 IAbsencesService absenceService)
        {
            this.studentService = studentService;
            this.absenceService = absenceService;
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
            var result = await absenceService.Add(model);

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
            var model = await absenceService.GetAbsenceses(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(model);
        }
    }
}
