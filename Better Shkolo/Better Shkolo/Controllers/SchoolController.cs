﻿using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.School;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.SchoolService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SchoolController : Controller
    {
        private IAccountService accountService;
        private ISchoolService schoolService;
        private ApplicationDbContext context;
        public SchoolController(IAccountService accountService
                                , ISchoolService schoolService
                                , ApplicationDbContext context)
        {
            this.accountService = accountService;
            this.schoolService = schoolService;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new SchoolCreateModel()
            {
                AvailableUsers = await accountService.GetAllAvailabeUsers()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(SchoolCreateModel model) 
        {
            if (!ModelState.IsValid)
            {
                model.AvailableUsers = await accountService.GetAllAvailabeUsers();

                return View(model);
            }

            var school = new School()
            {
                Name = model.Name,
                City = model.City,
                DirectorId = model.DirectorId
            };

            var result = await schoolService.AddSchool(school);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            model.AvailableUsers = await accountService.GetAllAvailabeUsers();

            ModelState.AddModelError("", "Someting went wrong!");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> View()
        {
            var allSchools = await schoolService.GetAllSchools();

            var model = new SchoolDisplayModel()
            {
                Schools = allSchools
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await schoolService.DeleteSchool(id);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(View));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var school = await schoolService.GetSchool(id);

            if (school == null)
            {
                return BadRequest();
            }

            var model = new SchoolCreateModel()
            {
                Name = school.Name,
                City = school.City,
                DirectorId = school.DirectorId,
                AvailableUsers = accountService.GetAllAvailabeUsers().Result
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SchoolCreateModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var school = await schoolService.GetSchool(id);

            school.Name = model.Name;
            school.City = model.City;
            school.DirectorId = model.DirectorId;

            context.SaveChanges();

            return RedirectToAction(nameof(View));
        }
    }
}
