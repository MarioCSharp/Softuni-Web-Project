using Better_Shkolo.Data;
using Better_Shkolo.Models.Api;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.StatisticsService
{
    public class StatisticsService : IStatisticsService
    {
        private ApplicationDbContext context;
        public StatisticsService(ApplicationDbContext context)
        {
            this.context = context;
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

            var marks = await context.Marks.Where(x => x.StudentId == student.Id).AverageAsync(x => x.Value);
            var absenceses = await context.Absencess.CountAsync(x => x.StudentId == student.Id);
            var reviews = await context.Reviews.CountAsync(x => x.StudentId == student.Id);
            var tests = await context.Tests.CountAsync(x => x.GradeId == student.GradeId);

            var model = new StatisticsDisplayModel()
            {
                Success = marks,
                Tests = tests,
                Absenceses = absenceses,
                Reviews = reviews
            };

            if (marks == 0)
            {
                model.Success = 0.0;
            }

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
}
