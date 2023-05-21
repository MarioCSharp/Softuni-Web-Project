using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.School;

namespace Better_Shkolo.Services.SchoolService
{
    public interface ISchoolService
    {
        Task<bool> AddSchool(School school);
        Task<bool> DeleteSchool(int id);
        Task<School> GetSchool(int id);
        Task<List<SchoolViewModel>> GetAllSchools();
    }
}
