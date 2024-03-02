using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Table;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
                    var a = new Subject()
                    {
                        Id = subject.Id,
                        SchoolId = subject.SchoolId,
                        GradeId = subject.GradeId,
                        TeacherId = subject.TeacherId,
                    };

                    s.Add(a);
                }
            }

            var shuffle = s.OrderBy(_ => Guid.NewGuid()).ToList();

            var schedule = new List<Table>();

            var sum = schoolStudyPlan.Sum(x => x.Amount);
            var used = new HashSet<Subject>();

            while (schedule.Count != sum)
            {
                foreach (var plan in shuffle)
                {
                    if (used.Contains(plan))
                    {
                        continue;
                    }

                    var found = false;

                    for (int day = 0; day < 5; day++)
                    {
                        for (int period = 0; period < 7; period++)
                        {
                            var toReEnter = new List<Subject>();

                            var asd = schedule
                                .FirstOrDefault(x => x.GradeId == plan.GradeId
                                && x.Day == day + 1 && x.Period == period + 1);

                            var asdd = schedule.FirstOrDefault(
                                x => x.TeacherId == plan.TeacherId && x.Day == day + 1 && period == period + 1);

                            if (asd == null && asdd == null)
                            {
                                var table = new Table()
                                {
                                    SubjectId = plan.Id,
                                    TeacherId = plan.TeacherId,
                                    GradeId = plan.GradeId,
                                    Day = day + 1,
                                    Period = period + 1
                                };

                                found = true;
                                schedule.Add(table);
                                used.Add(plan);
                            }

                            if (found)
                            {
                                break;
                            }

                            shuffle = s.OrderBy(_ => Guid.NewGuid()).ToList();
                        }

                        if (found)
                        {
                            break;
                        }
                    }
                }
            }

            ;

            await context.Tables.AddRangeAsync(schedule);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<TableViewModel> GetSchedule(int gradeId)
        {
            var table = await context.Tables
                .Where(x => x.GradeId == gradeId)
                .Select(x => new TableDisplayModel
                {
                    Day = x.Day,
                    Period = x.Period,
                    SubjectId = x.SubjectId,
                    SubjectName = x.Subject.Name,
                    TeacherId = x.TeacherId,
                    TeacherName = x.Teacher.User.FirstName + " " + x.Teacher.User.LastName
                }).ToListAsync();

            return new TableViewModel()
            {
                GradeId = gradeId,
                Tables = table
            };
        }
    }
}