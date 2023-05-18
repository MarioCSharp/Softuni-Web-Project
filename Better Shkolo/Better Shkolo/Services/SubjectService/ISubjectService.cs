using Better_Shkolo.Models.Subject;

namespace Better_Shkolo.Services.SubjectService
{
    public interface ISubjectService
    {
        Task<bool> Create(SubjectCreateModel model);
    }
}
