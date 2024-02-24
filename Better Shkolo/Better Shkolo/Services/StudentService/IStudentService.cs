using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Absence;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.Parent;
using Better_Shkolo.Models.Student;

namespace Better_Shkolo.Services.StudentService
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
    }
}
