using Better_Shkolo.Data;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.TableService
{
    public class TableService : ITableService
    {
        private ApplicationDbContext context;
        public TableService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> GenerateProgram(int schoolId)
        {
            var school = await context.Schools.FindAsync(schoolId);

            if (school is null) return false;

            var schoolStudyPlan = await context.StudyPlans
                .Where(x => x.SchoolId == schoolId).ToListAsync();

            var gradeByStudyPlan = new Dictionary<int, List<(int SubjectId, int Amount)>>();

            foreach (var plan in schoolStudyPlan)
            {
                if (!gradeByStudyPlan.ContainsKey(plan.Subject.GradeId))
                {
                    gradeByStudyPlan[plan.Subject.GradeId] = new List<(int SubjectId, int Amount)>();
                }

                gradeByStudyPlan[plan.Subject.GradeId].Add((plan.SubjectId, plan.Amount));
            }

            return true;
        }
    }
}