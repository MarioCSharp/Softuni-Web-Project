using Better_Shkolo.Models;
using Better_Shkolo.Models.Account;
using Better_Shkolo.Services.GradeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Better_Shkolo.Controllers
{
    public class HomeController : Controller
    {
        private IGradeService gradeService;
        public HomeController(IGradeService gradeService)
        {
            this.gradeService = gradeService;
        }
        [Authorize]
        public IActionResult Index()
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
                var model = new HomeModel()
                {
                    GradeId = gradeService.GetUserGradeId().Result 
                };

                return View(model);
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