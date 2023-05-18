using Better_Shkolo.Models.Subject;
using Better_Shkolo.Services.GradeService;
using Better_Shkolo.Services.SubjectService;
using Better_Shkolo.Services.TeacherService;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class SubjectController : Controller
    {
        private ITeacherService teacherService;
        private IGradeService gradeService;
        private ISubjectService subjectService;
        public SubjectController(ITeacherService teacherService,
                                 IGradeService gradeService,
                                 ISubjectService subjectService)
        {
            this.teacherService = teacherService;
            this.gradeService = gradeService;
            this.subjectService = subjectService;
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            var model = new SubjectCreateModel()
            {
                SchoolId = id,
                TeachersInSchool = teacherService.GetAllTeacherInSchool(id),
                GradesInSchool = gradeService.GetGradesBySchoolId(id)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(SubjectCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                model.TeachersInSchool = teacherService.GetAllTeacherInSchool(model.SchoolId);
                model.GradesInSchool = gradeService.GetGradesBySchoolId(model.SchoolId);

                return View(model);
            }

            var result = subjectService.Create(model).Result;

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }
    }
}
