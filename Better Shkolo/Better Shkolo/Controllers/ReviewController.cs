using Better_Shkolo.Models.Review;
using Better_Shkolo.Services.ReviewService;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class ReviewController : Controller
    {
        private IReviewService reviewService;
        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }
        [HttpGet]
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
    }
}
