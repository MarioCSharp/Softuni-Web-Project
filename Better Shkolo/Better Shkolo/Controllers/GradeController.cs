using Better_Shkolo.Models.Grade;
using Better_Shkolo.Services.GradeService;
using Better_Shkolo.Services.TeacherService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Data;

namespace Better_Shkolo.Controllers
{
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
        [Authorize(Policy = "CanEditDeleteAndCreateGrades")]
        public async Task<IActionResult> Create(int id)
        {
            var model = new GradeCreateModel()
            {
                SchoolId = id,
                Teachers = await teacherService.GetAllTeacherInSchool(id)
            };

            foreach (var item in model.Teachers.ToArray())
            {
                if (context.Grades.Any(x => x.TeacherId == item.Id))
                {
                    model.Teachers.Remove(item);
                }
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "CanEditDeleteAndCreateGrades")]
        public async Task<IActionResult> Create(GradeCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teachers = await teacherService.GetAllTeacherInSchool(model.SchoolId);
                return View(model);
            }

            var result = await gradeService.Create(model);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong! Try again later.");
            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "CanEditDeleteAndCreateGrades")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await gradeService.DeleteGrade(id);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(View));
        }

        [HttpGet]
        [Authorize(Policy = "CanEditDeleteAndCreateGrades")]
        public async Task<IActionResult> Edit(int id)
        {
            var grade = await gradeService.GetGrade(id);

            if (grade == null)
            {
                return BadRequest();
            }

            var model = new GradeCreateModel()
            {
                GradeName = grade.GradeName,
                GradeSpecialty = grade.GradeSpecialty,
                TeacherId = grade.TeacherId,
                Teachers = await teacherService.GetAllTeacherInSchool(grade.SchoolId)
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "CanEditDeleteAndCreateGrades")]
        public async Task<IActionResult> Edit(GradeCreateModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var school = await gradeService.GetGrade(id);

            school.GradeName = model.GradeName;
            school.GradeSpecialty = model.GradeSpecialty;
            school.TeacherId = model.TeacherId;

            context.SaveChanges();

            return RedirectToAction(nameof(View));
        }

        [HttpGet]
        [Authorize(Policy = "CanViewGrades")]
        public async Task<IActionResult> View(int id)
        {
            var model = new GradeViewModel()
            {
                Grades = await gradeService.GetGradesBySchoolId(id),
            };

            return View(model);
        }
    }
}
