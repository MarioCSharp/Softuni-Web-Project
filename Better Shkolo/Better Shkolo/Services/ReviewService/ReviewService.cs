using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Review;
using Better_Shkolo.Services.TeacherService;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private ApplicationDbContext context;
        private ITeacherService teacherService;
        public ReviewService(ApplicationDbContext context,
                             ITeacherService teacherService)
        {
            this.context = context;
            this.teacherService = teacherService;
        }
        public async Task<bool> Add(ReviewAddModel model)
        {
            var count = await context.Reviews.CountAsync();

            var teacher = await teacherService.GetTeacher();

            if (teacher is null)
            {
                return false;   
            }

            var review = new Review()
            {
                Description = model.Description,
                AddedOn = DateTime.Now,
                SubjectId = model.SubjectId,
                TeacherId = teacher.Id,
                StudentId = model.StudentId,
                SchoolId = teacher.SchoolId
            };

            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();

            return count + 1 == await context.Reviews.CountAsync();
        }
    }
}
