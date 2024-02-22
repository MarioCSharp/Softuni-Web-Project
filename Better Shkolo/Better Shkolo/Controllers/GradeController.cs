using Better_Shkolo.Models.Grade;
using Better_Shkolo.Services.GradeService;
using Better_Shkolo.Services.TeacherService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Data;
using AutoMapper;
using System.Security.Claims;

namespace Better_Shkolo.Controllers
{
    public class GradeController : Controller
    {
        private ITeacherService teacherService;
        private IGradeService gradeService;
        private IAccountService accountService;
        private ApplicationDbContext context;
        public IMapper mapper;
        public GradeController(ITeacherService teacherService,
                               IGradeService gradeService,
                               IAccountService accountService,
                               ApplicationDbContext context,
                               IMapper mapper)
        {
            this.teacherService = teacherService;
            this.gradeService = gradeService;
            this.accountService = accountService;
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(Policy = "AdministratorDirectorPolicy")]
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
        [Authorize(Policy = "AdministratorDirectorPolicy")]
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
        [Authorize(Policy = "AdministratorDirectorPolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await gradeService.DeleteGrade(id);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Policy = "AdministratorDirectorPolicy")]
        public async Task<IActionResult> Edit(int id)
        {
            var grade = await gradeService.GetGrade(id);

            if (grade == null)
            {
                return BadRequest();
            }

            var model = mapper.Map<GradeCreateModel>(grade);

            model.Teachers = await teacherService.GetAllTeacherInSchool(grade.SchoolId);

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "AdministratorDirectorPolicy")]
        public async Task<IActionResult> Edit(GradeCreateModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var grade = await gradeService.GetGrade(id);

            grade.GradeName = model.GradeName;
            grade.GradeSpecialty = model.GradeSpecialty;
            grade.TeacherId = model.TeacherId;

            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Policy = "AdministratorDirectorPolicy")]
        public async Task<IActionResult> View(int id)
        {
            var model = new GradeViewModel()
            {
                Grades = await gradeService.GetGradesBySchoolId(id),
            };

            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "AdministratorDirectorTeacherPolicy")]
        public async Task<IActionResult> Students(int id)
        {
            if (id != 0)
            {
                var model = await gradeService.GetStudentsInGrade(id);

                return View(model);
            }

            var isGradeTeacher = await accountService.IsGradeTeacher();

            if (!isGradeTeacher)
            {
                return BadRequest();
            }

            var studentsInGrade = await gradeService.GetStudentsInGrade(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(studentsInGrade);
        }
        [HttpGet]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> Statistics()
        {
            var teacher = await teacherService.GetTeacher();

            if (teacher is null)
            {
                return BadRequest();
            }

            var grade = await gradeService.GetGradeByTeacherId(teacher.Id);

            if (grade is null)
            {
                return BadRequest();
            }

            var model = await gradeService.GetGradeStatistics(grade);

            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> MyGrades()
        {
            var getTeacherGrades = await teacherService.GetGrades();

            return View(getTeacherGrades);
        }
    }
}
