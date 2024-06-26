﻿using BetterShkolo.Models.Review;
using BetterShkolo.Services.ReviewService;
using BetterShkolo.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Controllers
{
    public class ReviewController : Controller
    {
        private IReviewService reviewService;
        private IStudentService studentService;
        public ReviewController(IReviewService reviewService, IStudentService studentService)
        {
            this.reviewService = reviewService;
            this.studentService = studentService;
        }
        [HttpGet]
        [Authorize(Policy = "TeacherPolicy")]
        public IActionResult Add(int id, int subjectId)
        {
            var model = new ReviewAddModel()
            {
                SubjectId = subjectId,
                StudentId = id
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> Add(ReviewAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await reviewService.Add(model);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }
        [HttpGet]
        [Authorize(Policy = "StudentParentTeacherPolicy")]
        public async Task<IActionResult> View(string userId = null)
        {
            var model = await reviewService.GetReviews(userId);

            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "StudentParentTeacherPolicy")]
        public async Task<IActionResult> Display(int subjectId, string userId = null)
        {
            var model = await reviewService.GetReviewsBySubjectId(subjectId, userId);

            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "TeacherPolicy")]
        public async Task<IActionResult> ById(int studentId)
        {
            var student = await studentService.GetStudent(studentId);

            return RedirectToAction("View", "Review", new { userId = student.UserId });
        }

        [HttpGet]
        [Authorize(Policy = "StudentParentTeacherPolicy")]
        public async Task<IActionResult> Show(int studentId, int subjectId, string userId)
        {
            var student = await studentService.GetStudent(userId);

            if (student is null)
            {
                student = await studentService.GetStudentFromParent(userId);
            }

            return RedirectToAction("Display", "Review", new { subjectId, userId = student.UserId });
        }
    }
}
