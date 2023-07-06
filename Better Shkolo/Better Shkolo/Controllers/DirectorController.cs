using Better_Shkolo.Services.DirectorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    [Authorize(Policy = "CanAccessDirectorMenu")]
    public class DirectorController : Controller
    {
        private IDirectorService directorService;

        public DirectorController(IDirectorService directorService)
        {
            this.directorService = directorService;
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
    }
}
