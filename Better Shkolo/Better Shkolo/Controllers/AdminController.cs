using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        
    }
}
