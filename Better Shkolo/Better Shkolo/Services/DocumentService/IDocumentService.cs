using Better_Shkolo.Models.Document;

namespace Better_Shkolo.Services.DocumentService
{
    public interface IDocumentService
    {
        Task<bool> AddAsync(List<IFormFile> file, DocumentAddModel model);
        Task<List<DocumentIndexModel>> GetFilesInSchool(int schoolId);
        Task<byte[]> GetFile(int documentId);
    }
}
