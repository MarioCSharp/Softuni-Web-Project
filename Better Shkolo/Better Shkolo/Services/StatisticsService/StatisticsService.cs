using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Api;
using Better_Shkolo.Models.Application;
using Better_Shkolo.Models.Mark;
using Microsoft.Extensions.Caching.Memory;

namespace Better_Shkolo.Services.StatisticsService
{
    public class StatisticsService : IStatisticsService
    {
        private ApplicationDbContext context;
        private IMemoryCache memoryCache;
        public StatisticsService(ApplicationDbContext context,
                                 IMemoryCache memoryCache)
        {
            this.context = context;
            this.memoryCache = memoryCache;
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
    }

    public class CustomKVP : IComparable<CustomKVP>
    {
        public CustomKVP()
        {

        }
        public CustomKVP(int key, double value, Student student, User user)
        {
            this.Key = key;
            this.Value = value;
            Student = student;
            User = user;
        }
        public int Key { get; set; }
        public double Value { get; set; }
        public Student Student { get; set; }
        public User User { get; set; }
        public int Place { get; set; }

        public int CompareTo(CustomKVP? other)
        {
            return this.Key == other.Key ? 1 : -1;
        }

        public override bool Equals(object? obj)
        {
            return ((CustomKVP)obj).Key == this.Key;
        }
    }
}
