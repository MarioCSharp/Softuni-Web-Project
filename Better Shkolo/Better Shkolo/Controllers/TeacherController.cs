using Better_Shkolo.Data;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.Teacher;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.TeacherService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    [Authorize(Roles = "Administrator,Director")]
    public class TeacherController : Controller
    {
        private IAccountService accountService;
        private ITeacherService teacherService;
        private ApplicationDbContext context;
        public TeacherController(IAccountService accountService,
                                 ITeacherService teacherService,
                                 ApplicationDbContext context)
        {
            this.accountService = accountService;
            this.teacherService = teacherService;
            this.context = context;
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            var model = new TeacherCreateModel()
            {
                Users = accountService.GetAllAvailabeUsers().Result,
                SchoolId = id
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(TeacherCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Users = accountService.GetAllAvailabeUsers().Result;
                return View(model);
            }

            var result = teacherService.Create(model);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = teacherService.DeleteTeacher(id);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
