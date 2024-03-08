using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Table;
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

        public async Task<string> GetCurrentPeriod(string userId)
        {
            var student = await context.Students.FirstOrDefaultAsync(x => x.UserId == userId);

            var table = await GetSchedule(student.GradeId);
            var res = "-";
            var period = 0;

            var time = DateTime.Now.ToString("h:mm");
            var startTime = DateTime.Parse("08:00:00");
            var endTime = DateTime.Parse("08:45:00");
            var currentTime = DateTime.Parse(time);

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 1;
            }

            startTime = DateTime.Parse("09:05:00");
            endTime = DateTime.Parse("09:50:00");

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 2;
            }

            startTime = DateTime.Parse("10:00:00");
            endTime = DateTime.Parse("10:45:00");

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 3;
            }

            startTime = DateTime.Parse("10:55:00");
            endTime = DateTime.Parse("11:40:00");

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 4;
            }

            startTime = DateTime.Parse("11:50:00");
            endTime = DateTime.Parse("12:35:00");

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 5;
            }

            startTime = DateTime.Parse("12:45:00");
            endTime = DateTime.Parse("13:30:00");

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 6;
            }

            startTime = DateTime.Parse("13:40:00");
            endTime = DateTime.Parse("14:25:00");

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 7;
            }

            if (period == 0)
            {
                return res;
            }

            var day = (int)DateTime.Now.DayOfWeek;

            if (day > 5)
            {
                return res;
            }

            foreach (var t in table.Tables)
            {
                if (t.Day == day && t.Period == period)
                {
                    return t.SubjectName;
                }
            }

            return res;
        }

        public async Task<string> GetNextPeriod(string userId)
        {
            var student = await context.Students.FirstOrDefaultAsync(x => x.UserId == userId);

            var table = await GetSchedule(student.GradeId);

            var res = "-";

            var day = (int)DateTime.Now.DayOfWeek;

            if (day > 5)
            {
                return table.Tables.FirstOrDefault(x => x.Day == 1 && x.Period == 1).SubjectName;
            }


            var time = DateTime.Now.ToString("h:mm");
            var startTime = DateTime.Parse("08:00:00");
            var endTime = DateTime.Parse("08:45:00");
            var currentTime = DateTime.Parse(time);

            if (currentTime > DateTime.Parse("14:25:00"))
            {
                day++;

                if (day > 5)
                {
                    var nx = table.Tables.FirstOrDefault(x => x.Day == 1 && x.Period == 1);
                    return nx == null ? "-" : nx.SubjectName;
                }

                var next = table.Tables.FirstOrDefault(x => x.Day == day && x.Period == 1);

                return next == null ? table.Tables.FirstOrDefault(x => x.Day == 1 && x.Period == 1).SubjectName : next.SubjectName;
            }

            var period = 0;

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 1;
            }

            startTime = DateTime.Parse("09:05:00");
            endTime = DateTime.Parse("09:50:00");

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 2;
            }

            startTime = DateTime.Parse("10:00:00");
            endTime = DateTime.Parse("10:45:00");

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 3;
            }

            startTime = DateTime.Parse("10:55:00");
            endTime = DateTime.Parse("11:40:00");

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 4;
            }

            startTime = DateTime.Parse("11:50:00");
            endTime = DateTime.Parse("12:35:00");

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 5;
            }

            startTime = DateTime.Parse("12:45:00");
            endTime = DateTime.Parse("13:30:00");

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 6;
            }

            startTime = DateTime.Parse("13:40:00");
            endTime = DateTime.Parse("14:25:00");

            if (startTime <= currentTime && currentTime <= endTime)
            {
                period = 7;
            }

            if (period == 7)
            {
                day++;

                if (day > 5)
                {
                    var nx = table.Tables.FirstOrDefault(x => x.Day == 1 && x.Period == 1);
                    return nx == null ? "-" : nx.SubjectName;
                }

                var next = table.Tables.FirstOrDefault(x => x.Day == day && x.Period == 1);

                return next == null ? table.Tables.FirstOrDefault(x => x.Day == 1 && x.Period == 1).SubjectName : next.SubjectName;
            }

            period++;

            var n = table.Tables.FirstOrDefault(x => x.Day == day && x.Period == period);

            res = n == null ? "-" : n.SubjectName;

            return res;
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