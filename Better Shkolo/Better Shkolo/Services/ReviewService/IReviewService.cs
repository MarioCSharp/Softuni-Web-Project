using BetterShkolo.Models.Review;

namespace BetterShkolo.Services.ReviewService
{
    public interface IReviewService
    {
        Task<bool> Add(ReviewAddModel model);
        Task<List<ReviewDisplayModel>> GetReviews(string userId = null);
        Task<List<ReviewViewModel>> GetReviewsBySubjectId(int subjectId, string userId = null);
    }
}
