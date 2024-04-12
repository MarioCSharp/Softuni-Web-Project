using BetterShkolo.Models.Mark;

namespace BetterShkolo.Services.MarkService
{
    public interface IMarkService
    {
        Task<bool> Add(MarkAddModel model, int subjectId, string userId);
        Task<List<MarkDisplayModel>> GetMarks(string userId);
        Task<bool> AddTermMark(TermMarkAddModel model);
        Task<bool> AddYearMark(YearMarkAddModel model);
    }
}
