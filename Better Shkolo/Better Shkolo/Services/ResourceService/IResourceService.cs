using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Resource;

namespace Better_Shkolo.Services.ResourceService
{
    public interface IResourceService
    {
        Task<bool> AddResource(List<IFormFile> file, ResourceModel model);
        Task<Resource> GetResource(int resourceId);
    }
}
