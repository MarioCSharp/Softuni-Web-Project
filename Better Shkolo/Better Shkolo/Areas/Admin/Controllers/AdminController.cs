﻿using BetterShkolo.Models.School;
using BetterShkolo.Services.SchoolService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdministratorPolicy")]
    public class AdminController : Controller
    {
        private ISchoolService schoolService;
        public AdminController(ISchoolService schoolService)
        {
            this.schoolService = schoolService;
        }
        public IActionResult Menu()
        {
            return View();
        }

        public async Task<IActionResult> Manage()
        {
            var allSchools = await schoolService.GetAllSchools();

            var model = new SchoolDisplayModel()
            {
                Schools = allSchools
            };

            return View(model);
        }
    }
}
