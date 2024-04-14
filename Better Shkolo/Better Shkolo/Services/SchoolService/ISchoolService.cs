using BetterShkolo.Models.Activity;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.School;

namespace BetterShkolo.Services.SchoolService
{
    public interface ISchoolService
    {
        Task<bool> AddSchool(School school);
        Task<bool> DeleteSchool(int id);
        Task<School> GetSchool(int id);
        Task<List<SchoolViewModel>> GetAllSchools();
        Task<int> GetSchoolIdByUser();
        
    }
}
