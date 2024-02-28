using Better_Shkolo.Models.StudyPlan;

namespace Better_Shkolo.Services.StudyPlanService
{
    public interface IStudyPlanService
    {
        Task<StudyPlanChoseGradeModel> GetGradesInSchool(int schoolId);
        Task<List<StudyPlanCreateModel>> GetSubjectsInGrade(int gradeId);
        Task<bool> Create(List<StudyPlanCreateModel> model);
    }
}
