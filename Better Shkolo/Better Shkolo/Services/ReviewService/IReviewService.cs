using Better_Shkolo.Models.Review;

namespace Better_Shkolo.Services.ReviewService
{
    public interface IReviewService
    {
        Task<bool> Add(ReviewAddModel model);
        Task<List<ReviewDisplayModel>> GetReviews();
        Task<List<ReviewViewModel>> GetReviewsBySubjectId(int subjectId);
    }
}
