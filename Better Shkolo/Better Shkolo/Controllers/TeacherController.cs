using AutoMapper;
using Better_Shkolo.Data;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.Teacher;
using Better_Shkolo.Services.AbsenceService;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.GradeService;
using Better_Shkolo.Services.MarkService;
using Better_Shkolo.Services.StudentService;
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
        private IAbsencesService absencesService;
        private ITeacherService teacherService;
        private ISubjectService subjectService;
        private IStudentService studentService;
        private IGradeService gradeService;
        private IMarkService markService;
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
                                 IMarkService markService)
        {
            this.accountService = accountService;
            this.teacherService = teacherService;
            this.gradeService = gradeService;
            this.subjectService = subjectService;
            this.context = context;
            this.mapper = mapper;
            this.studentService = studentService;
            this.absencesService = absencesService;
            this.markService = markService;
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

            var model = mapper.Map<GradeDeleteModel>(grade);

            model.Teachers = await teacherService.GetAllTeacherInSchool(teacher.SchoolId);
            model.Teachers = model.Teachers.Where(x => x.Id != teacher.Id).ToList();
            model.OldTeacherId = teacher.Id;

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
        [HttpGet]
        [AllowAnonymous]
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
        [AllowAnonymous]
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
    }
}
