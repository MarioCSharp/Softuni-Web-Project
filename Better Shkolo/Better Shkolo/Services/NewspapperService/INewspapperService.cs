using Better_Shkolo.Models.Newspapper;

namespace Better_Shkolo.Services.NewspapperService
{
    public interface INewspapperService
    {
        Task<bool> PostAsync(List<IFormFile> Image, NewspapperAddModel model);
        Task<List<NewspaperIndexModel>> GetNews();
    }
}
