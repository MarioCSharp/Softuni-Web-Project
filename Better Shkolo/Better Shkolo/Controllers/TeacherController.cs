using Better_Shkolo.Data;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.Teacher;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.GradeService;
using Better_Shkolo.Services.SubjectService;
using Better_Shkolo.Services.TeacherService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    [Authorize(Policy = "CanAccessTeachers")]
    public class TeacherController : Controller
    {
        private IAccountService accountService;
        private ITeacherService teacherService;
        private ISubjectService subjectService;
        private IGradeService gradeService;
        private ApplicationDbContext context;
        public TeacherController(IAccountService accountService,
                                 ITeacherService teacherService,
                                 IGradeService gradeService,
                                 ISubjectService subjectService,
                                 ApplicationDbContext context)
        {
            this.accountService = accountService;
            this.teacherService = teacherService;
            this.gradeService = gradeService;
            this.subjectService = subjectService;
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var model = new TeacherCreateModel()
            {
                Users = await accountService.GetAllAvailabeUsers(),
                SchoolId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeacherCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Users = await accountService.GetAllAvailabeUsers();
                return View(model);
            }

            var result = teacherService.Create(model).Result;

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await teacherService.GetTeacher(id);
            var grade = await gradeService.GetGradeByTeacherId(teacher.Id);
            var subjects = await subjectService.GetSubjectsByTeacherId(teacher.Id);

            if (subjects.Count > 0)
            {
                return RedirectToAction("ToEdit", "Subject", new { id = teacher.Id });
            }

            if (grade is null)
            {
                var result = await teacherService.DeleteTeacher(id, 0);

                if (!result)
                {
                    return BadRequest();
                }

                return RedirectToAction("Index", "Home");
            }

            var model = new GradeDeleteModel()
            {
                GradeName = grade.GradeName,
                GradeSpecialty = grade.GradeSpecialty,
                SchoolId = grade.SchoolId,
                Teachers = await teacherService.GetAllTeacherInSchool(teacher.SchoolId),
                OldTeacherId = teacher.Id
            };

            model.Teachers = model.Teachers.Where(x => x.Id != teacher.Id).ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(GradeDeleteModel model)
        {
            var grade = await gradeService.GetGradeByTeacherId(model.OldTeacherId);

            grade.TeacherId = model.TeacherId;
            await context.SaveChangesAsync();

            var result = await teacherService.DeleteTeacher(model.OldTeacherId, model.TeacherId);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var model = new TeacherViewModel()
            {
                Teachers = await teacherService.GetAllTeacherInSchool(id)
            };

            return View(model);
        }
    }
}
