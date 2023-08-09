using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Api;
using Better_Shkolo.Models.Application;
using Better_Shkolo.Models.Mark;
using Microsoft.EntityFrameworkCore;
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

        public async Task<MarkInformationModel> GetMarkById(int id)
        {
            var mark = await context.Marks.FindAsync(id);

            if (mark is null)
            {
                return null;
            }

            var markInfo = memoryCache.Get($"Mark{id}");

            if (markInfo is null)
            {
                var teacher = await context.Teachers.FindAsync(mark.TeacherId);
                var teacherUser = await context.Users.FindAsync(teacher.UserId);

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

        public async Task<ApplicationStatisticsModel> GetApplicationStatistics()
        {
            var statistics = memoryCache.Get<ApplicationStatisticsModel>("ApplicationStatistics");

            if (statistics is null)
            {
                statistics = new ApplicationStatisticsModel()
                {
                    Schools = await context.Schools.CountAsync(),
                    Users = await context.Users.CountAsync(),
                    Directors = await context.Directors.CountAsync(),
                    Teachers = await context.Teachers.CountAsync(),
                    Students = await context.Students.CountAsync(),
                    Marks = await context.Marks.CountAsync(),
                    Reviews = await context.Reviews.CountAsync(),
                    Tests = await context.Tests.CountAsync(),
                    Absences = await context.Absencess.CountAsync(),
                    Grades = await context.Grades.CountAsync(),
                    Subjects = await context.Subjects.CountAsync()
                };

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                memoryCache.Set("ApplicationStatistics", statistics, cacheOptions);
            }

            return statistics;
        }

        public async Task<StatisticsDisplayModel> GetStatistics(string userId)
        {
            var student = await context.Students.FirstOrDefaultAsync(x => x.UserId == userId);

            if (student == null)
            {
                var parent = await context.Parents.FirstOrDefaultAsync(x => x.UserId == userId);
                student = await context.Students.FirstOrDefaultAsync(x => x.ParentId == parent.Id);
            }

            if (student == null)
            {
                return null;
            }

            var absenceses = await context.Absencess.CountAsync(x => x.StudentId == student.Id);
            var reviews = await context.Reviews.CountAsync(x => x.StudentId == student.Id);
            var tests = await context.Tests.CountAsync(x => x.GradeId == student.GradeId);

            var schoolOrder = memoryCache.Get($"SchoolPlaces{student.SchoolId}");

            if (schoolOrder is null)
            {
                var studentsInSchool = await context.Students.Where(x => x.SchoolId == student.SchoolId).ToListAsync();

                var marksAvarageSchool = new List<CustomKVP>();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(45));

                foreach (var current in studentsInSchool)
                {
                    var avarage = await context.Marks
                                    .Where(x => x.StudentId == current.Id)
                                    .AverageAsync(x => x.Value);

                    var kvp = new CustomKVP(current.Id, avarage, current, await context.Users.FindAsync(current.UserId));

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

        public async Task<ApplicationStatisticsModel> GetSchoolStatistics(int schoolId)
        {
            var statistics = memoryCache.Get<ApplicationStatisticsModel>($"SchoolStatistics{schoolId}");

            if (statistics is null)
            {
                statistics = new ApplicationStatisticsModel()
                {
                    Teachers = await context.Teachers.CountAsync(x => x.SchoolId == schoolId),
                    Students = await context.Students.CountAsync(x => x.SchoolId == schoolId),
                    Marks = await context.Marks.CountAsync(x => x.SchoolId == schoolId),
                    Reviews = await context.Reviews.CountAsync(x => x.SchoolId == schoolId),
                    Tests = await context.Tests.CountAsync(x => x.SchoolId == schoolId),
                    Absences = await context.Absencess.CountAsync(x => x.SchoolId == schoolId),
                    Grades = await context.Grades.CountAsync(x => x.SchoolId == schoolId),
                    Subjects = await context.Subjects.CountAsync(x => x.SchoolId == schoolId),
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
