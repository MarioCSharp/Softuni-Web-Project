using Better_Shkolo.Models.Mark;

namespace Better_Shkolo.Services.MarkService
{
    public interface IMarkService
    {
        Task<bool> Add(MarkAddModel model, int subjectId, string userId);
        Task<List<MarkDisplayModel>> GetMarks(string userId);
    }
}
