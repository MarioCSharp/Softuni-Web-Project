using BetterShkolo.Models.Document;

namespace BetterShkolo.Services.DocumentService
{
    public interface IDocumentService
    {
        Task<bool> AddAsync(List<IFormFile> file, DocumentAddModel model);
        Task<List<DocumentIndexModel>> GetFilesInSchool(int schoolId);
        Task<byte[]> GetFile(int documentId);
        Task<bool> Delete(int documentId);
        Task<string> GetExtension(int documentId);
    }
}
