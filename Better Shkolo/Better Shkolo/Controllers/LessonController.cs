﻿using Better_Shkolo.Models.Lesson;
using Better_Shkolo.Services.LessonService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class LessonController : Controller
    {
        private ILessonService lessonService;
        public LessonController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }

        [HttpGet]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> Add(int subjectId)
        {
            return View(new LessonAddModel()
            {
                SubjectId = subjectId
            });
        }

        [HttpPost]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> Add(LessonAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await lessonService.AddAsync(model);

            if (!res) return BadRequest();

            return RedirectToAction("Index", "Home");
         }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> View(int subjectId)
        {
            var mdl = await lessonService.GetLessons(subjectId);

            return View(mdl);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ById(int lessonId)
        {
            var rscs = await lessonService.GetLesson(lessonId);

            return View(rscs);
        }
    }
}
