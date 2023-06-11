using Better_Shkolo.Data;
using Better_Shkolo.Models.Director;
using Better_Shkolo.Services.DirectorService;
using Better_Shkolo.Services.EmailSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Controllers
{
    [Authorize(Policy = "CanAccessDirectorMenu")]
    public class DirectorController : Controller
    {
        private IDirectorService directorService;
        private IEmailSender emailSender;
        private ApplicationDbContext context;

        public DirectorController(IDirectorService directorService,
                                  IEmailSender emailSender,
                                  ApplicationDbContext context)
        {
            this.directorService = directorService;
            this.emailSender = emailSender;
            this.context = context;
        }

        public async Task<IActionResult> Menu()
        {
            var school = await directorService.GetSchoolByUser();

            return View(school);
        }

        public async Task<IActionResult> SubjectsView(int id)
        {
            return RedirectToAction("View", "Subject", new { id = id });
        }

        public async Task<IActionResult> SubjectAdd(int id)
        {
            return RedirectToAction("Create", "Subject", new { id = id });
        }

        public async Task<IActionResult> TeachersView(int id)
        {
            return RedirectToAction("View", "Teacher", new { id = id });
        }

        public async Task<IActionResult> TeacherAdd(int id)
        {
            return RedirectToAction("Create", "Teacher", new { id = id });
        }
        public async Task<IActionResult> GradesView(int id)
        {
            return RedirectToAction("View", "Grade", new { id = id });
        }
        public async Task<IActionResult> GradeAdd(int id)
        {
            return RedirectToAction("Create", "Grade", new { id = id });
        }
        public async Task<IActionResult> StudentsView(int id)
        {
            return RedirectToAction("View", "Student", new { id = id });
        }
        public async Task<IActionResult> StudentAdd(int id)
        {
            return RedirectToAction("Add", "Student", new { id = id });
        }
        [HttpGet]
        public async Task<IActionResult> EmailSend(int id)
        {
            var model = new EmailSendModel()
            {
                SchoolId = id
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EmailSend(EmailSendModel model)
        {
            var studentsInSchool = await context.Students.Where(x => x.SchoolId == model.SchoolId).ToListAsync();

            foreach (var student in studentsInSchool)
            {
                var user = await context.Users.FindAsync(student.UserId);
                await emailSender.SendEmailAsync(user.Email, model.Subject, model.Message);
            }

            return RedirectToAction(nameof(Menu));
        }
    }
}
