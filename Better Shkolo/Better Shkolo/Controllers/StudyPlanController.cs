﻿using BetterShkolo.Models.StudyPlan;
using BetterShkolo.Services.StudyPlanService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Controllers
{
    public class StudyPlanController : Controller
    {
        private IStudyPlanService studyPlanService;
        public StudyPlanController(IStudyPlanService studyPlanService)
        {
            this.studyPlanService = studyPlanService;
        }
        [Authorize(Roles = "Director")]
        [HttpGet]
        public async Task<IActionResult> Chose(int id)
        {
            var model = await studyPlanService.GetGradesInSchool(id);

            return View(model);
        }
        [Authorize(Roles = "Director")]
        [HttpPost]
        public async Task<IActionResult> Chose(StudyPlanChoseGradeModel model)
        {
            return RedirectToAction("Create", "StudyPlan", new { gradeId = model.GradeId });
        }
        [Authorize(Roles = "Director")]
        [HttpGet]
        public async Task<IActionResult> Create(int gradeId)
        {
            var subjectsInGrade = await studyPlanService.GetSubjectsInGrade(gradeId);

            return View(subjectsInGrade);
        }
        [Authorize(Roles = "Director")]
        [HttpPost]
        public async Task<IActionResult> Create(List<StudyPlanCreateModel> model)
        {
            var result = await studyPlanService.Create(model);

            if (!result) return View(model);

            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Director")]
        public async Task<IActionResult> Plans(int id)
        {
            var model = await studyPlanService.GetStudyPlans(id);

            return View(model);
        }
        [Authorize(Roles = "Director")]
        public async Task<IActionResult> Details(int gradeId)
        {
            var model = await studyPlanService.GetDetails(gradeId);

            return View(model);
        }
        [Authorize(Roles = "Director")]
        public async Task<IActionResult> Edit(int gradeId)
        {
            var model = await studyPlanService.GetDetails(gradeId);

            return View(model);
        }
        [Authorize(Roles = "Director")]
        [HttpPost]
        public async Task<IActionResult> Edit(List<StudyPlanCreateModel> model)
        {
            await studyPlanService.Edit(model);

            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Director")]
        [HttpGet]
        public async Task<IActionResult> Delete(int gradeId)
        {
            await studyPlanService.Delete(gradeId);

            return RedirectToAction("Index", "Home");
        }
    }
}
