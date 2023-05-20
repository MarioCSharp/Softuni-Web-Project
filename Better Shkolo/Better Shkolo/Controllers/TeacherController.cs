using Better_Shkolo.Data;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.Subject;
using Better_Shkolo.Models.Teacher;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.GradeService;
using Better_Shkolo.Services.SubjectService;
using Better_Shkolo.Services.TeacherService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Better_Shkolo.Controllers
{
    [Authorize(Roles = "Administrator,Director")]
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
        public IActionResult Create(int id)
        {
            var model = new TeacherCreateModel()
            {
                Users = accountService.GetAllAvailabeUsers().Result,
                SchoolId = id
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(TeacherCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Users = accountService.GetAllAvailabeUsers().Result;
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
        public IActionResult Delete(int id)
        {
            var teacher = teacherService.GetTeacher(id);
            var grade = gradeService.GetGradeByTeacherId(teacher.Id);
            var subjects = subjectService.GetSubjectsByTeacherId(teacher.Id);

            if (subjects.Count > 0)
            {
                return RedirectToAction("ToEdit", "Subject", new { id = teacher.Id });
            }

            var model = new GradeDeleteModel()
            {
                GradeName = grade.GradeName,
                GradeSpecialty = grade.GradeSpecialty,
                SchoolId = grade.SchoolId,
                Teachers = teacherService.GetAllTeacherInSchool(teacher.SchoolId).Where(x => x.Id != teacher.Id).ToList(),
                OldTeacherId = teacher.Id
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(GradeDeleteModel model)
        {
            var grade = gradeService.GetGradeByTeacherId(model.OldTeacherId);

            grade.TeacherId = model.TeacherId;
            await context.SaveChangesAsync();

            var result = teacherService.DeleteTeacher(model.OldTeacherId).Result;

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            var model = new TeacherViewModel()
            {
                Teachers = teacherService.GetAllTeacherInSchool(id)
            };

            return View(model);
        }
    }
}
