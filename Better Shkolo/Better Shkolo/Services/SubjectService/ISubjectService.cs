using BetterShkolo.Data.Models;
using BetterShkolo.Models.Subject;

namespace BetterShkolo.Services.SubjectService
{
    public interface ISubjectService
    {
        Task<bool> Create(SubjectCreateModel model);
        Task<List<SubjectDisplayModel>> GetSubjectsByTeacherId(int id);
        Task<List<SubjectDisplayModel>> GetSubjectsBySchoolId(int Id);
        Task<Subject> GetSubject(int id);
        Task<bool> DeleteSubject(int id);
        Task<List<SubjectDisplayModel>> GetSubjectsByUser();
        Task<List<SubjectDisplayModel>> GetSubjectsByGrade(int gradeId);

        Task<bool> Edit(SubjectCreateModel model, int id);
    }
}
