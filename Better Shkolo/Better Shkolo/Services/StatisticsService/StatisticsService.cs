using BetterShkolo.Data;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.Api;
using BetterShkolo.Models.Application;
using BetterShkolo.Models.Mark;
using BetterShkolo.Models.Teacher;
using BetterShkolo.Services.SchoolService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static BetterShkolo.Data.Constants;

namespace BetterShkolo.Services.StatisticsService
{
    public class StatisticsService : IStatisticsService
    {
        private ApplicationDbContext context;
        private ISchoolService schoolService;
        private IMemoryCache memoryCache;
        public StatisticsService(ApplicationDbContext context,
                                 IMemoryCache memoryCache,
                                 ISchoolService schoolService)
        {
            this.context = context;
            this.memoryCache = memoryCache;
            this.schoolService = schoolService;
        }

        public MarkInformationModel GetMarkById(int id)
        {
            var mark = context.Marks.Find(id);

            if (mark is null)
            {
                return null;
            }

            var markInfo = memoryCache.Get($"Mark{id}");

            if (markInfo is null)
            {
                var teacher = context.Teachers.Find(mark.TeacherId);
                var teacherUser = context.Users.Find(teacher.UserId);

                var addedOn = mark.AddedOn.ToString("MM/dd/yyyy HH:mm:ss");

                var result = new MarkInformationModel()
                {
                    Value = mark.Value,
                    AddedOn = addedOn,
                    TeacherName = $"{teacherUser.FirstName} {teacherUser.LastName}"
                };

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.Low);

                memoryCache.Set($"Mark{id}", result, cacheOptions);

                return result;
            }

            return (MarkInformationModel)markInfo;
        }

        public ApplicationStatisticsModel GetApplicationStatistics()
        {
            var statistics = memoryCache.Get<ApplicationStatisticsModel>("ApplicationStatistics");

            if (statistics is null)
            {
                statistics = new ApplicationStatisticsModel()
                {
                    Schools = context.Schools.Count(),
                    Users = context.Users.Count(),
                    Directors = context.Directors.Count(),
                    Teachers = context.Teachers.Count(),
                    Students = context.Students.Count(),
                    Marks = context.Marks.Count(),
                    Reviews = context.Reviews.Count(),
                    Tests = context.Tests.Count(),
                    Absences = context.Absencess.Count(),
                    Grades = context.Grades.Count(),
                    Subjects = context.Subjects.Count()
                };

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                memoryCache.Set("ApplicationStatistics", statistics, cacheOptions);
            }

            return statistics;
        }

        public StatisticsDisplayModel GetStatistics(string userId)
        {
            var student = context.Students.FirstOrDefault(x => x.UserId == userId);

            if (student == null)
            {
                var parent = context.Parents.FirstOrDefault(x => x.UserId == userId);
                if (parent is null)
                {
                    return null;
                }
                student = context.Students.FirstOrDefault(x => x.ParentId == parent.Id);
            }

            if (student == null)
            {
                return null;
            }

            var absenceses = context.Absencess.Count(x => x.StudentId == student.Id);
            var reviews = context.Reviews.Count(x => x.StudentId == student.Id);
            var tests = context.Tests.Count(x => x.GradeId == student.GradeId);

            var schoolOrder = memoryCache.Get($"SchoolPlaces{student.SchoolId}");

            if (schoolOrder is null)
            {
                var studentsInSchool = context.Students.Where(x => x.SchoolId == student.SchoolId).ToList();

                var marksAvarageSchool = new List<CustomKVP>();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(45));

                foreach (var current in studentsInSchool)
                {
                    var marks = context.Marks
                                    .Where(x => x.StudentId == current.Id).ToList();

                    var kvp = new CustomKVP();

                    if (marks.Count == 0)
                    {
                        kvp = new CustomKVP(current.Id, 0, current, context.Users.Find(current.UserId));

                        marksAvarageSchool.Add(kvp);

                        memoryCache.Set($"Student{current.Id}", 0, cacheOptions);
                        continue;
                    }

                    var avarage = marks.Average(x => x.Value);

                    kvp = new CustomKVP(current.Id, avarage, current, context.Users.Find(current.UserId));

                    marksAvarageSchool.Add(kvp);

                    memoryCache.Set($"Student{current.Id}", avarage, cacheOptions);
                }

                var place = 1;

                foreach (var current in marksAvarageSchool.OrderByDescending(x => x.Value))
                {
                    current.Place = place++;
                }

                memoryCache.Set($"SchoolPlaces{student.SchoolId}", marksAvarageSchool.ToList(), cacheOptions);
            }

            double studentMarks = 0.0;
            var schoolPlaces = new List<CustomKVP>();

            memoryCache.TryGetValue($"Student{student.Id}", out studentMarks);
            memoryCache.TryGetValue($"SchoolPlaces{student.SchoolId}", out schoolPlaces);
            studentMarks = Math.Round(studentMarks, 2);

            var model = new StatisticsDisplayModel()
            {
                Success = studentMarks,
                Tests = tests,
                Absenceses = absenceses,
                Reviews = reviews,
            };

            if (absenceses == 0)
            {
                model.Absenceses = 0;
            }

            if (reviews == 0)
            {
                model.Reviews = 0;
            }

            if (tests == 0)
            {
                model.Tests = 0;
            }

            return model;
        }

        public ApplicationStatisticsModel GetSchoolStatistics(int schoolId)
        {
            var statistics = memoryCache.Get<ApplicationStatisticsModel>($"SchoolStatistics{schoolId}");

            if (statistics is null)
            {
                statistics = new ApplicationStatisticsModel()
                {
                    Teachers = context.Teachers.Count(x => x.SchoolId == schoolId),
                    Students = context.Students.Count(x => x.SchoolId == schoolId),
                    Marks = context.Marks.Count(x => x.SchoolId == schoolId),
                    Reviews = context.Reviews.Count(x => x.SchoolId == schoolId),
                    Tests = context.Tests.Count(x => x.SchoolId == schoolId),
                    Absences = context.Absencess.Count(x => x.SchoolId == schoolId),
                    Grades = context.Grades.Count(x => x.SchoolId == schoolId),
                    Subjects = context.Subjects.Count(x => x.SchoolId == schoolId),
                };

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                memoryCache.Set($"SchoolStatistics{schoolId}", statistics, cacheOptions);
            }

            return statistics;
        }

        public async Task<TeacherHomeModel> GetTeacherHomeModel()
        {
            var mdl = new TeacherHomeModel();

            var sId = await schoolService.GetSchoolIdByUser();

            var scss = await context.Marks.Where(x => x.SchoolId == sId).ToListAsync();
            var scs = 0.0;
            if (scss.Count > 0)
            {
                scs = scss.Average(x => x.Value);
            }

            scs = double.Parse($"{scs:F2}");

            var abs = await context.Absencess.Where(x => x.SchoolId == sId).CountAsync();
            var rvs = await context.Reviews.Where(x => x.SchoolId == sId).CountAsync();
            var tst = await context.Tests.Where(x => x.SchoolId == sId).CountAsync();

            var marks = await context.Marks.Where(x => x.SchoolId == sId).ToListAsync();

            var standings = new Dictionary<int, double>();

            foreach (var mark in marks)
            {
                var s = await context.Subjects.FindAsync(mark.SubjectId);

                if (!standings.ContainsKey(s.GradeId))
                {
                    standings[s.GradeId] = marks.Where(x => context.Subjects.Find(x.SubjectId).GradeId == s.GradeId).Average(x => x.Value);
                }
            }

            if (standings.Count < 3)
            {
                standings.Add(0, 0.0);
                standings.Add(-1, 0.0);
                standings.Add(-2, 0.0);
            }

            var fS = standings.OrderByDescending(x => x.Value).ToArray()[0];
            var sS = standings.OrderByDescending(x => x.Value).ToArray()[1];
            var tS = standings.OrderByDescending(x => x.Value).ToArray()[2];

            var absences = await context.Absencess.Where(x => x.SchoolId == sId).ToListAsync();

            var aStandings = new Dictionary<int, int>();

            foreach (var a in absences)
            {
                var s = await context.Subjects.FindAsync(a.SubjectId);

                if (!aStandings.ContainsKey(s.GradeId))
                {
                    aStandings[s.GradeId] = absences.Where(x => context.Subjects.Find(x.SubjectId).GradeId == s.GradeId).Count();
                }
            }

            if (aStandings.Count < 3)
            {
                aStandings.Add(0, 0);
                aStandings.Add(-1, 0);
                aStandings.Add(-2, 0);
            }

            var fA = aStandings.OrderByDescending(x => x.Value).ToArray()[0];
            var sA = aStandings.OrderByDescending(x => x.Value).ToArray()[1];
            var tA = aStandings.OrderByDescending(x => x.Value).ToArray()[2];

            mdl.Success = scs;
            mdl.Absences = abs;
            mdl.Reviews = rvs;
            mdl.Tests = tst;

            if (fS.Key <= 0)
            {
                mdl.FirstPlaceSuccess = ("-", 0.0);
            }
            else
            {
                var g = await context.Grades.FindAsync(fS.Key);

                mdl.FirstPlaceSuccess = (g.GradeName, double.Parse($"{fS.Value:F2}"));
            }
            if (sS.Key <= 0)
            {
                mdl.SecondPlaceSuccess = ("-", 0.0);
            }
            else
            {
                var g = await context.Grades.FindAsync(sS.Key);
                mdl.SecondPlaceSuccess = (g.GradeName, double.Parse($"{sS.Value:F2}"));
            }
            if (tS.Key <= 0)
            {
                mdl.ThirdPlaceSuccess = ("-", 0.0);
            }
            else
            {
                var g = await context.Grades.FindAsync(tS.Key);
                mdl.ThirdPlaceSuccess = (g.GradeName, double.Parse($"{tS.Value:F2}"));
            }

            if (fA.Key <= 0)
            {
                mdl.FirstPlaceAbsences = ("-", 0);
            }
            else
            {
                var g = await context.Grades.FindAsync(fA.Key);
                mdl.FirstPlaceAbsences = (g.GradeName, fA.Value);
            }
            if (sA.Key <= 0)
            {
                mdl.SecondPlaceAbsences = ("-", 0);
            }
            else
            {
                var g = await context.Grades.FindAsync(sA.Key);
                mdl.SecondPlaceAbsences = (g.GradeName, sA.Value);
            }
            if (tA.Key <= 0)
            {
                mdl.ThirdPlaceSuccess = ("-", 0);
            }
            else
            {
                var g = await context.Grades.FindAsync(tA.Key);
                mdl.ThirdPlaceAbsences = (g.GradeName, tA.Value);
            }

            return mdl;
        }
    }

    public class CustomKVP : IComparable<CustomKVP>
    {
        public CustomKVP()
        {

        }
        public CustomKVP(int key, double value, Student student, Data.Models.User user)
        {
            Key = key;
            Value = value;
            Student = student;
            User = user;
        }
        public int Key { get; set; }
        public double Value { get; set; }
        public Student Student { get; set; }
        public Data.Models.User User { get; set; }
        public int Place { get; set; }

        public int CompareTo(CustomKVP? other)
        {
            return Key == other.Key ? 1 : -1;
        }

        public override bool Equals(object? obj)
        {
            return ((CustomKVP)obj).Key == Key;
        }
    }
}
