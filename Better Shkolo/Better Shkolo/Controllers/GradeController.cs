using Better_Shkolo.Models.Grade;
using Better_Shkolo.Services.GradeService;
using Better_Shkolo.Services.TeacherService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Data;

namespace Better_Shkolo.Controllers
{
    [Authorize(Roles = "Administrator,Director")]
    public class GradeController : Controller
    {
        private ITeacherService teacherService;
        private IGradeService gradeService;
        private IAccountService accountService;
        private ApplicationDbContext context;

        public GradeController(ITeacherService teacherService,
                               IGradeService gradeService,
                               IAccountService accountService,
                               ApplicationDbContext context)
        {
            this.teacherService = teacherService;
            this.gradeService = gradeService;
            this.accountService = accountService;
            this.context = context;
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            var model = new GradeCreateModel()
            {
                SchoolId = id,
                Teachers = teacherService.GetAllTeacherInSchool(id, accountService.GetUserId())
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(GradeCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teachers = teacherService.GetAllTeacherInSchool(model.SchoolId, accountService.GetUserId());
                return View(model);
            }

            var result = gradeService.Create(model).Result;

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong! Try again later.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = gradeService.DeleteGrade(id);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var grade = gradeService.GetGrade(id);

            if (grade == null)
            {
                return BadRequest();
            }

            var model = new GradeCreateModel()
            {
                GradeName = grade.GradeName,
                GradeSpecialty = grade.GradeSpecialty,
                TeacherId = grade.TeacherId
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(GradeCreateModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var school = gradeService.GetGrade(id);

            school.GradeName = model.GradeName;
            school.GradeSpecialty = model.GradeSpecialty;
            school.TeacherId = model.TeacherId;

            context.SaveChanges();

            return RedirectToAction(nameof(View));
        }
    }
}
