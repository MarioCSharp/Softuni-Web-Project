﻿using AutoMapper;
using Better_Shkolo.Models.Parent;
using Better_Shkolo.Models.Student;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.GradeService;
using Better_Shkolo.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class StudentController : Controller
    {
        private IStudentService studentService;
        private IAccountService accountService;
        private IGradeService gradeService;
        private IMapper mapper;
        public StudentController(IStudentService studentService,
                                 IAccountService accountService,
                                 IGradeService gradeService,
                                 IMapper mapper)
        {
            this.studentService = studentService;
            this.accountService = accountService;
            this.gradeService = gradeService;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(Policy = "CanAccessStudents")]
        public async Task<IActionResult> Add(int id)
        {
            var model = new StudentCreateModel()
            {
                SchoolId = id,
                Users = await accountService.GetAllAvailabeUsers(),
                Grades = await gradeService.GetGradesBySchoolId(id),
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "CanAccessStudents")]
        public async Task<IActionResult> Add(StudentCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Users = await accountService.GetAllAvailabeUsers();
                model.Grades = await gradeService.GetGradesBySchoolId(model.SchoolId);
                return View(model);
            }

            var result = await studentService.Add(model);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            model.Users = await accountService.GetAllAvailabeUsers();
            model.Grades = await gradeService.GetGradesBySchoolId(model.SchoolId);

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "CanAccessStudents")]
        public async Task<IActionResult> View(int id)
        {
            var model = new StudentViewModel()
            {
                Students = await studentService.GetStudentsInSchool(id)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AsignParent(int id)
        {
            var model = new ParentCreateModel()
            {
                Users = await accountService.GetAllAvailabeUsers()
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "CanAccessStudents")]
        public async Task<IActionResult> AsignParent(ParentCreateModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                model.Users = await accountService.GetAllAvailabeUsers();
                return View(model);
            }

            var result = await studentService.AsignParent(model, id);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "CanAccessStudents")]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await studentService.GetStudent(id);

            var model = mapper.Map<StudentCreateModel>(student);

            model.Grades = await gradeService.GetGradesBySchoolId(student.SchoolId);

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "CanAccessStudents")]
        public async Task<IActionResult> Edit(StudentCreateModel model, int id)
        {
            var result = await studentService.Edit(model, id);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "CanAccessStudents")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await studentService.Delete(id);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            return BadRequest();
        }
        [HttpGet]
        [Authorize(Policy = "CanDisplayStudentsInSubject")]
        public async Task<IActionResult> Display(int id)
        {
            var model = new StudentViewModel()
            {
                Students = await studentService.GetStudentsInSubject(id),
                SubjectId = id
            };

            return View(model);
        }
    }
}
