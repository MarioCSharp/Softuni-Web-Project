using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.School;

namespace Better_Shkolo.Services.SchoolService
{
    public interface ISchoolService
    {
        Task<bool> AddSchool(School school);
        bool DeleteSchool(int id);
        School GetSchool(int id);
        List<SchoolViewModel> GetAllSchools();
    }
}
