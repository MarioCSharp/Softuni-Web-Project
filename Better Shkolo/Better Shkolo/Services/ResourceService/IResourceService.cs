using BetterShkolo.Data.Models;
using BetterShkolo.Models.Resource;

namespace BetterShkolo.Services.ResourceService
{
    public interface IResourceService
    {
        Task<bool> AddResource(List<IFormFile> file, ResourceModel model);
        Task<Resource> GetResource(int resourceId);
    }
}
