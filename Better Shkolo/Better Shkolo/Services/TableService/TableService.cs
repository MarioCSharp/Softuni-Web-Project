using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
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

            var s = new List<Subject>();

            foreach (var plan in schoolStudyPlan)
            {
                var subject = await context.Subjects.FindAsync(plan.SubjectId);

                for (int i = 0; i < plan.Amount; i++)
                {
                    s.Add(subject);
                }
            }

            var shuffle = s.OrderBy(_ => Guid.NewGuid()).ToList();

            var schedule = new List<Table>();

            while (shuffle.Any())
            {
                for (int day = 0; day < 5; day++)
                {
                    for (int period = 0; period < 8; period++)
                    {
                        var toReEnter = new List<Subject>();
                        var toRemove = new List<Subject>();

                        foreach (var plan in shuffle)
                        {
                            if (!schedule
                                .Any(x => x.GradeId != plan.GradeId
                                && x.TeacherId != plan.TeacherId
                                && x.Day != day && x.Period != period))
                            {
                                var table = new Table()
                                {
                                    SubjectId = plan.Id,
                                    TeacherId = plan.TeacherId,
                                    GradeId = plan.GradeId,
                                    Day = day + 1,
                                    Period = period + 1
                                };

                                schedule.Add(table);
                                toRemove.Add(plan);
                            }
                            else
                            {
                                toReEnter.Add(plan);
                            }
                        }

                        foreach (var rE in toReEnter)
                        {
                            shuffle.Remove(rE);
                            shuffle.Add(rE);
                        }

                        foreach (var tR in toRemove)
                        {
                            shuffle.Remove(tR);
                        }

                        shuffle = s.OrderBy(_ => Guid.NewGuid()).ToList();
                    }
                }
            }

            await context.Tables.AddRangeAsync(schedule);

            return true;
        }
    }
}