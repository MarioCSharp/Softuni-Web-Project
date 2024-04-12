using BetterShkolo.Models.Grade;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.Absences;
using BetterShkolo.Models.Parent;
using BetterShkolo.Models.Student;

namespace BetterShkolo.Services.StudentService
{
    public interface IStudentService
    {
        Task<bool> Add(StudentCreateModel model);
        Task<List<StudentDisplayModel>> GetStudentsInSchool(int id);
        Task<bool> AsignParent(ParentCreateModel model, int id);
        Task<bool> Edit(StudentCreateModel model, int id);
        Task<Student> GetStudent(int id);
        Task<Student> GetStudent(string id);
        Task<bool> Delete(int id);
        Task<List<StudentDisplayModel>> GetStudentsInSubject(int id);
        Task<AbsencesAddModel> GetStudentModel(int id);
        Task<StudentProfileModel> GetStudentProfile(string userId, int term);
        Task<StudentViewModel> GetStudentsInGrade(int id);
        Task<(string, double)> GetBestSubject(int place);
        Task<Student> GetStudentFromParent(string userId);
    }
}
