using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Subject;

namespace Better_Shkolo.Services.SubjectService
{
    public interface ISubjectService
    {
        Task<bool> Create(SubjectCreateModel model);
        List<SubjectDisplayModel> GetSubjectsByTeacherId(int id);
        List<SubjectDisplayModel> GetSubjectsBySchoolId(int Id);
        Subject GetSubject(int id);
        Task<bool> DeleteSubject(int id);
    }
}
