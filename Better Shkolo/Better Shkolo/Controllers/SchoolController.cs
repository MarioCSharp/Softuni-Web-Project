using AutoMapper;
using BetterShkolo.Data.Models;
using BetterShkolo.Data;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.School;
using BetterShkolo.Services.AccountService;
using BetterShkolo.Services.SchoolService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Controllers
{
    public class SchoolController : Controller
    {
        private IAccountService accountService;
        private ISchoolService schoolService;
        private ApplicationDbContext context;
        private IMapper mapper;
        private UserManager<User> userManager;
        public SchoolController(IAccountService accountService
                                , ISchoolService schoolService
                                , ApplicationDbContext context
                                , IMapper mapper,
                                 UserManager<User> userManager)
        {
            this.accountService = accountService;
            this.schoolService = schoolService;
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> Add()
        {
            var model = new SchoolCreateModel()
            {
                AvailableUsers = await accountService.GetAllAvailabeUsers()
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "AdministratorPolicy")]
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
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await schoolService.DeleteSchool(id);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Policy = "AdministratorDirectorPolicy")]
        public async Task<IActionResult> Edit(int id)
        {
            var school = await schoolService.GetSchool(id);

            if (school == null)
            {
                return BadRequest();
            }

            var model = mapper.Map<SchoolCreateModel>(school);

            model.AvailableUsers = await accountService.GetAllAvailabeUsers();

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "AdministratorDirectorPolicy")]
        public async Task<IActionResult> Edit(SchoolCreateModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var school = await schoolService.GetSchool(id);

            if (school.DirectorId != model.DirectorId)
            {
                await userManager.AddToRoleAsync(await context.Users.FindAsync(model.DirectorId), "Director");
                await userManager.RemoveFromRoleAsync(await context.Users.FindAsync(school.DirectorId), "Director");
            }

            school.Name = model.Name;
            school.City = model.City;
            school.DirectorId = model.DirectorId;

            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
