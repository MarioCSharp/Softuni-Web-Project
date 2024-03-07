using Better_Shkolo.Models;
using Better_Shkolo.Models.Account;
using Better_Shkolo.Services.GradeService;
using Better_Shkolo.Services.StandingsService;
using Better_Shkolo.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Better_Shkolo.Controllers
{
    public class HomeController : Controller
    {
        private IGradeService gradeService;
        private IStudentService studentService;
        private IStandingsService standingsService;
        public HomeController(IGradeService gradeService,
                              IStudentService studentService,
                              IStandingsService standingsService)
        {
            this.gradeService = gradeService;
            this.studentService = studentService;
            this.standingsService = standingsService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Teacher"))
            {
                return RedirectToAction("MyGrades", "Grade");
            }
            else if (User.IsInRole("Director"))
            {
                return RedirectToAction("Menu", "Director", new { area = "Director" });
            }
            else if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("Menu", "Admin", new { area = "Admin" });
            }

            if (User.IsInRole("Student") || User.IsInRole("Parent"))
            {
                var stundentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var student = await studentService.GetStudent(stundentUserId);

                var model = await standingsService.GetPlaces(student);

                var res = new HomeModel()
                {
                    GradeId = gradeService.GetUserGradeId().Result ,
                    PlaceInSchool = model.PlaceSchool,
                    PlaceInGrade = model.PlaceGrade,
                    PlaceInYear = model.PlaceYear
                };

                return View(res);
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}