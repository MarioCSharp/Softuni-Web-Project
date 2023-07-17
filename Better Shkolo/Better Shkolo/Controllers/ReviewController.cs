using Better_Shkolo.Models.Review;
using Better_Shkolo.Services.ReviewService;
using Better_Shkolo.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
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
        [Authorize(Policy = "CanAddReviews")]
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
        [Authorize(Policy = "CanAddReviews")]
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
        [Authorize(Policy = "CanViewReviews")]
        public async Task<IActionResult> View(string userId = null)
        {
            var model = await reviewService.GetReviews(userId);

            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "CanViewReviews")]
        public async Task<IActionResult> Display(int subjectId, string userId = null)
        {
            var model = await reviewService.GetReviewsBySubjectId(subjectId, userId);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ById(int studentId)
        {
            var student = await studentService.GetStudent(studentId);

            return RedirectToAction("View", "Review", new { userId = student.UserId });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Show(int studentId, int subjectId)
        {
            var student = await studentService.GetStudent(studentId);

            return RedirectToAction("Display", "Review", new {subjectId = subjectId, userId = student.UserId});
        }
    }
}
