using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Standings;
using Better_Shkolo.Models.Student;
using Better_Shkolo.Services.StatisticsService;
using Microsoft.Extensions.Caching.Memory;

namespace Better_Shkolo.Services.StandingsService
{
    public class StandingsService : IStandingsService
    {
        private ApplicationDbContext context;
        private IMemoryCache memoryCache;
        private IStatisticsService statisticsService;

        public StandingsService(ApplicationDbContext context,
                                IMemoryCache memoryCache,
                                IStatisticsService statisticsService)
        {
            this.context = context;
            this.memoryCache = memoryCache;
            this.statisticsService = statisticsService;
        }

        public async Task<StandingsDisplayModel> GetStandings(Student student, string searchTerm)
        {
            var schoolPlaces = memoryCache.Get<List<CustomKVP>>($"SchoolPlaces{student.SchoolId}");

            var model = new StandingsDisplayModel();

            if (schoolPlaces is null)
            {
                statisticsService.GetStatistics(student.UserId);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    model.SchoolStandings = schoolPlaces
                        .OrderBy(x => x.Place)
                        .Where(x => (x.User.FirstName + " " + x.User.LastName).ToLower().Contains(searchTerm.ToLower()))
                        .Select(x => new StudentStandingsModel()
                        {
                            Success = Math.Round(x.Value, 2),
                            Id = x.Student.UserId,
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            Email = x.User.Email,
                            Student = x.Student,
                            Place = x.Place
                        }).ToList();
                }

                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    model.SchoolStandings = schoolPlaces.OrderBy(x => x.Place)
                        .Select(x => new StudentStandingsModel()
                        {
                            Success = Math.Round(x.Value, 2),
                            Id = x.Student.UserId,
                            FirstName = x.User.FirstName,
                            LastName = x.User.LastName,
                            Email = x.User.Email,
                            Student = x.Student,
                            Place = x.Place
                        }).ToList();
                }

                return model;
            }

            schoolPlaces = memoryCache.Get<List<CustomKVP>>($"SchoolPlaces{student.SchoolId}");

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                model.SchoolStandings = schoolPlaces.OrderBy(x => x.Place)
                    .Where(x => (x.User.FirstName + " " + x.User.LastName).ToLower().Contains(searchTerm.ToLower()))
                    .Select(x => new StudentStandingsModel()
                    {
                        Success = Math.Round(x.Value, 2),
                        Id = x.Student.UserId,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Email = x.User.Email,
                        Student = x.Student,
                        Place = x.Place
                    }).ToList();
            }


            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                model.SchoolStandings = schoolPlaces.OrderBy(x => x.Place)
                    .Select(x => new StudentStandingsModel()
                    {
                        Success = Math.Round(x.Value, 2),
                        Id = x.Student.UserId,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Email = x.User.Email,
                        Student = x.Student,
                        Place = x.Place
                    }).ToList();
            }

            return model;
        }
    }
}
