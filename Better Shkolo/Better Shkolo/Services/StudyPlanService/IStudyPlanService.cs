using BetterShkolo.Models.StudyPlan;

namespace BetterShkolo.Services.StudyPlanService
{
    public interface IStudyPlanService
    {
        Task<StudyPlanChoseGradeModel> GetGradesInSchool(int schoolId);
        Task<List<StudyPlanCreateModel>> GetSubjectsInGrade(int gradeId);
        Task<bool> Create(List<StudyPlanCreateModel> model);
        Task<StudyPlanViewModel> GetStudyPlans(int schoolId);
        Task<List<StudyPlanCreateModel>> GetDetails(int gradeId);
        Task<bool> Edit(List<StudyPlanCreateModel> model);
        Task<bool> Delete(int gradeId);
    }
}
