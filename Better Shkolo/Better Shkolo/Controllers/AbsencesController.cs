using Better_Shkolo.Models.Absence;
using Better_Shkolo.Services.AbsenceService;
using Better_Shkolo.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class AbsencesController : Controller
    {
        private IStudentService studentService;
        private IAbsenceService absenceService;
        public AbsencesController(IStudentService studentService,
                                 IAbsenceService absenceService)
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
        public async Task<IActionResult> Add(AbsencesAddModel model, int id)
        {
            var result = await absenceService.Add(model, id);

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
            var model = await absenceService.GetAbsenceses();

            return View(model);
        }
    }
}
