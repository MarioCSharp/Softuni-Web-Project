using BetterShkolo.Models.Newspapper;

namespace BetterShkolo.Services.NewspapperService
{
    public interface INewspapperService
    {
        Task<bool> PostAsync(List<IFormFile> Image, NewspapperAddModel model);
        Task<List<NewspaperIndexModel>> GetNews();
        Task<NewspaperIndexModel> GetPost(int id);
    }
}
