﻿using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.School;
using Better_Shkolo.Models.Subject;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.GradeService;
using Better_Shkolo.Services.SchoolService;
using Better_Shkolo.Services.SubjectService;
using Better_Shkolo.Services.TeacherService;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class SubjectController : Controller
    {
        private ITeacherService teacherService;
        private IGradeService gradeService;
        private ISubjectService subjectService;
        private ApplicationDbContext context;
        public SubjectController(ITeacherService teacherService,
                                 IGradeService gradeService,
                                 ISubjectService subjectService,
                                 ApplicationDbContext context)
        {
            this.teacherService = teacherService;
            this.gradeService = gradeService;
            this.subjectService = subjectService;
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var model = new SubjectCreateModel()
            {
                SchoolId = id,
                TeachersInSchool = await teacherService.GetAllTeacherInSchool(id),
                GradesInSchool = await gradeService.GetGradesBySchoolId(id)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubjectCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                model.TeachersInSchool = await teacherService.GetAllTeacherInSchool(model.SchoolId);
                model.GradesInSchool = await gradeService.GetGradesBySchoolId(model.SchoolId);

                return View(model);
            }

            var result = await subjectService.Create(model);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var model = new SubjectViewModel()
            {
                Subjects = await subjectService.GetSubjectsBySchoolId(id),
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ToEdit(int id)
        {
            var subjects = await subjectService.GetSubjectsByTeacherId(id);

            var model = new SubjectViewModel()
            {
                Subjects = subjects
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var subject = await subjectService.GetSubject(id);

            if (subject == null)
            {
                return BadRequest();
            }

            var model = new SubjectCreateModel()
            {
                Name = subject.Name,
                TeacherId = subject.TeacherId,
                SchoolId = subject.SchoolId,
                GradeId = subject.GradeId,
                TeachersInSchool = await teacherService.GetAllTeacherInSchool(subject.SchoolId),
                GradesInSchool = await gradeService.GetGradesBySchoolId(subject.SchoolId)
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SubjectCreateModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var subject = await subjectService.GetSubject(id);

            subject.Name = model.Name;
            subject.SchoolId = model.SchoolId;
            subject.TeacherId = model.TeacherId;
            subject.GradeId = model.GradeId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(View));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = subjectService.DeleteSubject(id).Result;

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
