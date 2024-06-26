﻿using BetterShkolo.Models.Activity;
using BetterShkolo.Services.ActivityService;
using BetterShkolo.Services.SchoolService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Controllers
{
    public class ActivityController : Controller
    {
        private IActivityService activityService;
        private ISchoolService schoolService;
        public ActivityController(IActivityService activityService,
                                  ISchoolService schoolService)
        {
            this.activityService = activityService;
            this.schoolService = schoolService;
        }
        [HttpGet]
        [Authorize(Policy = "DirectorTeacherPolicy")]
        public async Task<IActionResult> Add()
        {
            var model = new ActivityAddModel()
            {
                SchoolId = await schoolService.GetSchoolIdByUser()
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Policy = "DirectorTeacherPolicy")]
        public async Task<IActionResult> Add(ActivityAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await activityService.AddAsync(model);

            if (res == null) return BadRequest();

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize(Policy = "DirectorStudentParentTeacherPolicy")]
        public async Task<IActionResult> Schedule()
        {
            var schoolId = await schoolService.GetSchoolIdByUser();

            var result = await activityService.GetActivitiesInSchool(schoolId);

            return View(result);
        }
    }
}
