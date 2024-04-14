using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Erasmus;
using BetterShkolo.Data;
using BetterShkolo.Models.Erasmus;
using BetterShkolo.Models.Student;
using BetterShkolo.Services.SchoolService;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace BetterShkolo.Services.ErasmusService
{
    public class ErasmusService : IErasmusService
    {
        private ApplicationDbContext context;
        private ISchoolService schoolService;
        public ErasmusService(ApplicationDbContext context,
                              ISchoolService schoolService)
        {
            this.context = context;
            this.schoolService = schoolService;
        }

        public async Task Activate(int schoolId)
        {
            var school = await context.Schools.FindAsync(schoolId);

            school.ActiveErasmus = true;
            await context.SaveChangesAsync();
        }

        public async Task<bool> AddDocument(ErasmusDocumentAddModel model, IFormFile file)
        {
            var sId = await schoolService.GetSchoolIdByUser();

            var doc = new ErasmusDocument()
            {
                SchoolId = sId,
                Name = model.Name
            };

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                doc.File = stream.ToArray();
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                doc.FileExtension = Path.GetExtension(fileName);
            }

            await context.ErasmusDocuments.AddAsync(doc);
            await context.SaveChangesAsync();

            return await context.ErasmusDocuments.ContainsAsync(doc);
        }

        public Task<bool> Apply(ErasmusApplyModel model)
        {
            throw new NotImplementedException();
        }

        public async Task Deactivate(int schoolId)
        {
            var school = await context.Schools.FindAsync(schoolId);

            school.ActiveErasmus = false;
            await context.SaveChangesAsync();
        }

        public List<StudentDisplayModel> GetAligibleStudents(int schoolId)
        {
            var allStudents = context.Students
                .Where(s => s.SchoolId == schoolId && s.Marks.Average(x => x.Value) >= 4.50);

            return allStudents.Select(s => new StudentDisplayModel
            {
                Id = s.Id,
                FirstName = s.User.FirstName,
                LastName = s.User.LastName,
                Email = s.User.Email,
                UserId = s.UserId,
                SchoolId = s.SchoolId,
                SchoolName = s.School.Name
            }).ToList();
        }

        public async Task<ErasmusDocument> GetDoc(int documentId)
        {
            return await context.ErasmusDocuments.FindAsync(documentId);
        }

        public async Task<List<ErasmusDocumentIndexModel>> GetSchoolDocuments(int schoolId)
        {
            return await context.ErasmusDocuments
                .Where(x => x.SchoolId == schoolId)
                .Select(x => new ErasmusDocumentIndexModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();
        }

        public async Task<bool> SchoolIsActive(int schoolId)
        {
            var s = await context.Schools.FindAsync(schoolId);

            return s.ActiveErasmus;
        }
    }
}
