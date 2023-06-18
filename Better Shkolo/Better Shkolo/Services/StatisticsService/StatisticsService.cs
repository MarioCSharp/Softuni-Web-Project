using Better_Shkolo.Data;
using Better_Shkolo.Models.Api;
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
                var marksAvarageGrade = new List<CustomKVP>();


                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(45));

                foreach (var current in studentsInSchool)
                {
                    var avarage = await context.Marks
                                    .Where(x => x.StudentId == current.Id)
                                    .AverageAsync(x => x.Value);

                    var kvp = new CustomKVP(current.Id, avarage);

                    marksAvarageSchool.Add(kvp);

                    if (current.GradeId == student.GradeId)
                    {
                        marksAvarageGrade.Add(kvp);
                    }

                    memoryCache.Set($"Student{current.Id}", avarage, cacheOptions);
                }

                memoryCache.Set($"GradePlaces{student.GradeId}", marksAvarageGrade.OrderByDescending(x => x.Value).ToList(), cacheOptions);
                memoryCache.Set($"SchoolPlaces{student.SchoolId}", marksAvarageSchool.OrderByDescending(x => x.Value).ToList(), cacheOptions);
            }

            var studentMarks = memoryCache.Get<double>($"Student{student.Id}");
            var schoolPlaces = memoryCache.Get<List<CustomKVP>>($"SchoolPlaces{student.SchoolId}");
            var gradeOrder = memoryCache.Get<List<CustomKVP>>($"GradePlaces{student.GradeId}");
            studentMarks = Math.Round(studentMarks, 2);

            var studentKvp = new CustomKVP(student.Id, studentMarks);

            var placeSchool = memoryCache.Get($"StudentPlaceSchool{student.Id}");

            if (placeSchool is null)
            {
                memoryCache.Set($"StudentPlaceSchool{student.Id}", schoolPlaces.IndexOf(studentKvp) + 1);
                memoryCache.Set($"StudentPlaceGrade{student.Id}", gradeOrder.IndexOf(studentKvp) + 1);
            }

            var model = new StatisticsDisplayModel()
            {
                Success = studentMarks,
                Tests = tests,
                Absenceses = absenceses,
                Reviews = reviews,
                PlaceInGrade = memoryCache.Get<int>($"StudentPlaceGrade{student.Id}"),
                PlaceInSchool = memoryCache.Get<int>($"StudentPlaceSchool{student.Id}")
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
    }

    public class CustomKVP : IComparable<CustomKVP>
    {
        public CustomKVP(int key, double value)
        {
            this.Key = key;
            this.Value = value;
        }
        public int Key { get; set; }
        public double Value { get; set; }

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
