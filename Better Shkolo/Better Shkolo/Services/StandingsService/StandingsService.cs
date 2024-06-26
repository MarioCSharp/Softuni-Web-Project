﻿using BetterShkolo.Models.Student;
using BetterShkolo.Data;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.Standings;
using BetterShkolo.Services.StatisticsService;
using Microsoft.Extensions.Caching.Memory;

namespace BetterShkolo.Services.StandingsService
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

        public async Task<StandingsDisplayModel> GetPlaces(Student student)
        {
            var school = await GetStandings(student, "");

            var m = school.SchoolStandings.FirstOrDefault(x => x.Student.Id == student.Id);

            if (m == null) return new StandingsDisplayModel();

            var thisYear = "";

            var i = 0;

            var grade = await context.Grades.FindAsync(m.Student.GradeId);

            while (char.IsDigit(grade.GradeName[i]))
            {
                thisYear += grade.GradeName[i];
                i++;
            }

            var placeInGrade = 1;
            var placeInSchool = 1;
            var placeInYear = 1;

            foreach (var s in school.SchoolStandings)
            {
                if (s.Id == m.Id)
                {
                    continue;
                }

                if (s.Success > m.Success)
                {
                    placeInSchool++;
                }

                if (s.Student.GradeId == m.Student.GradeId && s.Success > m.Success)
                {
                    placeInGrade++;
                }

                var otherYear = "";
                i = 0;
                var cG = await context.Grades.FindAsync(s.Student.GradeId);

                while (char.IsDigit(cG.GradeName[i]))
                {
                    otherYear += cG.GradeName[i];
                    i++;
                }

                if (thisYear == otherYear && s.Success > m.Success)
                {
                    placeInYear++;
                }
            }

            return new StandingsDisplayModel
            {
                PlaceYear = placeInYear,
                PlaceSchool = placeInSchool,
                PlaceGrade = placeInGrade,
            };
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
