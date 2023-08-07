using Better_Shkolo.Models.School;
using Better_Shkolo.Services.DirectorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Areas.Director.Controllers
{
    [Area("Director")]
    [Authorize(Policy = "DirectorPolicy")]
    public class DirectorController : Controller
    {
        private IDirectorService directorService;
        public DirectorController(IDirectorService directorService)
        {
            this.directorService = directorService;
        }

        public async Task<IActionResult> Menu(int schoolId)
        {
            if (schoolId == 0)
            {
                var school = await directorService.GetSchoolByUser();

                if (school is null)
                {
                    return BadRequest();
                }

                return View(new SchoolMenuModel { SchoolId = school.Id });
            }

            return View(new SchoolMenuModel { SchoolId = schoolId });
        }
        
        public async Task<IActionResult> Manage()
        {
            if (!User.IsInRole("Director"))
            {
                return BadRequest();
            }

            var school = await directorService.GetSchoolByUser();

            return View(school);
        }
    }
}
