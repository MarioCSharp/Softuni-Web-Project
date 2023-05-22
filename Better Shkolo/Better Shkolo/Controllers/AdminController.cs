using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    [Authorize(Policy = "CanAccessAdminMenu")]
    public class AdminController : Controller
    {
        public IActionResult Menu()
        {
            return View();
        }
    }
}
