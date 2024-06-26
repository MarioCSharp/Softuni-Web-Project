﻿using Better_Shkolo.Models.Diploma;

namespace Better_Shkolo.Services.DiplomaService
{
    public interface IDiplomaService
    {
        Task<bool> AddDiploma(DiplomaAddModel model, IFormFile File);
        Task<DiplomaDisplayModel> GetSchoolDiplomas();
        Task<DiplomaDisplayModel> GetSchoolDiplomas(string docType);
        Task<DiplomaDisplayModel> GetSchoolDiplomas(DiplomaDisplayModel model);
    }
}
