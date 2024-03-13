using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Document;
using Better_Shkolo.Services.SchoolService;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Better_Shkolo.Services.DocumentService
{
    public class DocumentService : IDocumentService
    {
        private ApplicationDbContext context;
        private ISchoolService schoolService;
        public DocumentService(ApplicationDbContext context,
                               ISchoolService schoolService)
        {
            this.context = context;
            this.schoolService = schoolService;
        }

        public async Task<bool> AddAsync(List<IFormFile> file, DocumentAddModel model)
        {
            var school = await schoolService.GetSchoolIdByUser();

            var doc = new Document()
            {
                SchoolId = school,
                Name = model.Name,
            };

            foreach (var d in file)
            {
                if (d.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await d.CopyToAsync(stream);
                        doc.File = stream.ToArray();
                    }
                }
            }

            await context.Documents.AddRangeAsync(doc);
            await context.SaveChangesAsync();

            return await context.Documents.ContainsAsync(doc);
        }

        public async Task<byte[]> GetFile(int documentId)
        {
            var doc = await context.Documents.FindAsync(documentId);

            return doc.File;
        }

        public async Task<List<DocumentIndexModel>> GetFilesInSchool(int schoolId)
        {
            return await context.Documents
                .Where(x => x.SchoolId == schoolId)
                .Select(x => new DocumentIndexModel
                {
                    SchoolId = schoolId,
                    Name = x.Name,
                    File = x.File,
                    DocumentId = x.Id
                }).ToListAsync();
        }
    }
}
