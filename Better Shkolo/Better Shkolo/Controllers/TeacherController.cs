using AutoMapper;
using BetterShkolo.Data;
using BetterShkolo.Models.Grade;
using BetterShkolo.Models.Teacher;
using BetterShkolo.Services.AbsencesService;
using BetterShkolo.Services.AccountService;
using BetterShkolo.Services.GradeService;
using BetterShkolo.Services.MarkService;
using BetterShkolo.Services.StatisticsService;
using BetterShkolo.Services.StudentService;
using BetterShkolo.Services.SubjectService;
using BetterShkolo.Services.TeacherService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Controllers
{
    public class TeacherController : Controller
    {
        private IAccountService accountService;
        private IAbsencesService absencesService;
        private ITeacherService teacherService;
        private ISubjectService subjectService;
        private IStudentService studentService;
        private IGradeService gradeService;
        private IMarkService markService;
        private IStatisticsService statisticsService;
        private ApplicationDbContext context;
        private IMapper mapper;
        public TeacherController(IAccountService accountService,
                                 ITeacherService teacherService,
                                 IGradeService gradeService,
                                 ISubjectService subjectService,
                                 ApplicationDbContext context,
                                 IMapper mapper,
                                 IStudentService studentService,
                                 IAbsencesService absencesService,
                                 IMarkService markService,
                                 IStatisticsService statisticsService)
        {
            this.accountService = accountService;
            this.teacherService = teacherService;
            this.gradeService = gradeService;
            this.subjectService = subjectService;
            this.context = context;
            this.mapper = mapper;
            this.studentService = studentService;
            this.absencesService = absencesService;
            this.statisticsService = statisticsService;
            this.markService = markService;
        }
        [HttpGet]
        [Authorize(Policy = "AdministratorDirectorPolicy")]
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
        [Authorize(Policy = "AdministratorDirectorPolicy")]
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
        [Authorize(Policy = "AdministratorDirectorPolicy")]
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

            var model = mapper.Map<GradeDeleteModel>(grade);

            model.Teachers = await teacherService.GetAllTeacherInSchool(teacher.SchoolId);
            model.Teachers = model.Teachers.Where(x => x.Id != teacher.Id).ToList();
            model.OldTeacherId = teacher.Id;

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "AdministratorDirectorPolicy")]
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
        [Authorize(Policy = "AdministratorDirectorPolicy")]
        public async Task<IActionResult> View(int id)
        {
            var model = new TeacherViewModel()
            {
                Teachers = await teacherService.GetAllTeacherInSchool(id)
            };

            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> ManageGrade()
        {
            var isTeacher = await accountService.IsGradeTeacher();

            if (!isTeacher)
            {
                return BadRequest();
            }

            return View();
        }

        [HttpGet]
        [Authorize(Policy = "StudentParentPolicy")]
        public async Task<IActionResult> StudentMarks(int studentId)
        {
            var student = await studentService.GetStudent(studentId);

            if (student is null)
            {
                return BadRequest();
            }

            var studentMarks = await markService.GetMarks(student.UserId);

            return View(studentMarks);
        }
        [HttpGet]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> Home()
        {
            var mdl = await statisticsService.GetTeacherHomeModel();

            return View(mdl);
        }
    }
}
