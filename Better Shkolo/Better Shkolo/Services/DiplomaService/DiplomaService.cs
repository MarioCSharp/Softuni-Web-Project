using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Diploma;
using BetterShkolo.Data;
using BetterShkolo.Services.SchoolService;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Better_Shkolo.Services.DiplomaService
{
    public class DiplomaService : IDiplomaService
    {
        private ApplicationDbContext context;
        private ISchoolService schoolService;
        public DiplomaService(ApplicationDbContext context,
                              ISchoolService schoolService)
        {
            this.schoolService = schoolService;
            this.context = context;
        }

        public async Task<bool> AddDiploma(DiplomaAddModel model, IFormFile File)
        {
            var schoolId = await schoolService.GetSchoolIdByUser();
            var diploma = new Diploma()
            {
                EducationForm = model.EducationForm,
                Cancelled = false,
                FabricNumber = model.FabricNumber,
                FullName = model.FullName,
                Identification = model.Identification,
                IssuedDate = model.IssuedDate,
                RegistrationNumber = model.RegistrationNumber,
                SchoolYear = model.SchoolYear,
                Series = model.Series,
                Type = model.Type,
                YearRegistrationNumber = model.YearRegistrationNumber,
                SchoolId = schoolId
            };

            using (var stream = new MemoryStream())
            {
                await File.CopyToAsync(stream);
                diploma.File = stream.ToArray();
                string fileName = ContentDispositionHeaderValue.Parse(File.ContentDisposition).FileName.Trim('"');
                diploma.FileExtension = Path.GetExtension(fileName);
            }

            await context.Diplomas.AddAsync(diploma);
            await context.SaveChangesAsync();

            return await context.Diplomas.ContainsAsync(diploma);
        }

        public async Task<DiplomaDisplayModel> GetSchoolDiplomas()
        {
            var schoolId = await schoolService.GetSchoolIdByUser();

            return new DiplomaDisplayModel
            {
                Diplomas = await context.Diplomas
                .Where(x => x.SchoolId == schoolId)
                .Select(x => new DiplomaIndexModel()
                {
                    EducationForm = x.EducationForm,
                    FabricNumber = x.FabricNumber,
                    FullName = x.FullName,
                    Id = x.Id,
                    Identification = x.Identification,
                    IssuedDate = x.IssuedDate,
                    RegistrationNumber = x.RegistrationNumber,
                    SchoolYear = x.SchoolYear,
                    Series = x.Series,
                    Type = x.Type,
                    YearRegistrationNumber = x.YearRegistrationNumber
                }).ToListAsync()
            };
        }

        public async Task<DiplomaDisplayModel> GetSchoolDiplomas(string docType)
        {
            var schoolId = await schoolService.GetSchoolIdByUser();

            return new DiplomaDisplayModel
            {
                Diplomas = await context.Diplomas
                .Where(x => x.SchoolId == schoolId && docType == x.Type)
                .Select(x => new DiplomaIndexModel()
                {
                    EducationForm = x.EducationForm,
                    FabricNumber = x.FabricNumber,
                    FullName = x.FullName,
                    Id = x.Id,
                    Identification = x.Identification,
                    IssuedDate = x.IssuedDate,
                    RegistrationNumber = x.RegistrationNumber,
                    SchoolYear = x.SchoolYear,
                    Series = x.Series,
                    Type = x.Type,
                    YearRegistrationNumber = x.YearRegistrationNumber
                }).ToListAsync()
            };
        }

        public async Task<DiplomaDisplayModel> GetSchoolDiplomas(DiplomaDisplayModel model)
        {
            var schoolId = await schoolService.GetSchoolIdByUser();
            var diplomas = await context.Diplomas
                .Where(x => x.SchoolId == schoolId).ToListAsync();

            if (!string.IsNullOrWhiteSpace(model.DocType))
            {
                diplomas = diplomas.Where(x => x.Type == model.DocType).ToList();
            }
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                diplomas = diplomas.Where(x => x.FullName.ToLower().Contains(model.Name.ToLower())).ToList();
            }

            return new DiplomaDisplayModel
            {
                DocType = model.DocType,
                Name = model.Name,
                Diplomas = diplomas.Select(x => new DiplomaIndexModel()
                {
                    EducationForm = x.EducationForm,
                    FabricNumber = x.FabricNumber,
                    FullName = x.FullName,
                    Id = x.Id,
                    Identification = x.Identification,
                    IssuedDate = x.IssuedDate,
                    RegistrationNumber = x.RegistrationNumber,
                    SchoolYear = x.SchoolYear,
                    Series = x.Series,
                    Type = x.Type,
                    YearRegistrationNumber = x.YearRegistrationNumber
                }).ToList()
            };
        }
    }
}
