using Better_Shkolo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Better_Shkolo.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Teacher"))
            {
                return RedirectToAction("Manage", "Subject");
            }
            else if (User.IsInRole("Director"))
            {
                return RedirectToAction("Menu", "Director");
            }
            else if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("Menu", "Admin", new { area = "Admin" });
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