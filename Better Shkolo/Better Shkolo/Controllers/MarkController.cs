using Better_Shkolo.Models.Mark;
using Better_Shkolo.Services.MarkService;
using Better_Shkolo.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Better_Shkolo.Controllers
{
    public class MarkController : Controller
    {
        private IMarkService markService;
        private IStudentService studentService;
        public MarkController(IMarkService markService, IStudentService studentService)
        {
            this.markService = markService;
            this.studentService = studentService;
        }
        [HttpGet]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> Add(int id, int subjectId)
        {
            var model = new MarkAddModel()
            {
                StudentId = id,
                SubjectId = subjectId
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> Add(MarkAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await markService.Add(model, model.SubjectId, User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (result)
            {
                return RedirectToAction("Display", "Student", new { id = model.SubjectId });
            }

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "StudentParentPolicy")]
        public async Task<IActionResult> View()
        {
            var model = await markService.GetMarks(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> ById(int studentId)
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
        public async Task<IActionResult> AddTermMark(int subjectId, int studentId)
        {
            var model = new TermMarkAddModel()
            {
                StudentId = studentId,
                SubjectId = subjectId
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> AddTermMark(TermMarkAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await markService.AddTermMark(model);

            if(!result) return BadRequest();

            return RedirectToAction("Display", "Student", new { id = model.SubjectId });
        }
        [HttpGet]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> AddYearMark(int subjectId, int studentId)
        {
            var model = new YearMarkAddModel()
            {
                StudentId = studentId,
                SubjectId = subjectId
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> AddYearMark(YearMarkAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await markService.AddYearMark(model);

            if (!result) return BadRequest();

            return RedirectToAction("Display", "Student", new { id = model.SubjectId });
        }
    }
}
