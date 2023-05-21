using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Subject;

namespace Better_Shkolo.Services.SubjectService
{
    public interface ISubjectService
    {
        Task<bool> Create(SubjectCreateModel model);
        Task<List<SubjectDisplayModel>> GetSubjectsByTeacherId(int id);
        Task<List<SubjectDisplayModel>> GetSubjectsBySchoolId(int Id);
        Task<Subject> GetSubject(int id);
        Task<bool> DeleteSubject(int id);
    }
}
