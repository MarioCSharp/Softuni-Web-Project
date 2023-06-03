using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Mark;
using Better_Shkolo.Models.Review;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.TeacherService;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private ApplicationDbContext context;
        private ITeacherService teacherService;
        private IAccountService accountService;
        public ReviewService(ApplicationDbContext context,
                             ITeacherService teacherService,
                             IAccountService accountService)
        {
            this.context = context;
            this.teacherService = teacherService;
            this.accountService = accountService;

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

        public async Task<List<ReviewDisplayModel>> GetReviews()
        {
            var userId = accountService.GetUserId();

            var model = new List<ReviewDisplayModel>();

            var student = await context.Students.FirstOrDefaultAsync(x => x.UserId == userId);

            var studentId = 0;

            if (student == null)
            {
                var parent = await context.Parents.FirstOrDefaultAsync(x => x.UserId == userId);

                studentId = parent.StudentId;
            }
            else
            {
                studentId = student.Id;
            }

            if (studentId == 0)
            {
                return null;
            }

            var reviews = await context.Reviews.Where(x => x.StudentId == studentId).ToListAsync();

            foreach (var review in reviews)
            {
                var subjectId = review.SubjectId;

                if (!model.Any(x => x.SubjectId == subjectId))
                {
                    var subject = await context.Subjects.FindAsync(subjectId);

                    model.Add(new ReviewDisplayModel()
                    {
                        SubjectId = subjectId,
                        SubjectName = subject.Name,
                        Reviews = new List<ReviewViewModel>()
                    });


                }

                var teacher = await context.Teachers.FindAsync(review.TeacherId);
                var teacherUser = await context.Users.FindAsync(teacher.UserId);

                model.FirstOrDefault(x => x.SubjectId == subjectId).Reviews.Add(new ReviewViewModel()
                {
                    Id = review.Id,
                    Description = review.Description,
                    AddedOn = review.AddedOn,
                    TeacherId = teacher.Id,
                    TeacherName = teacherUser.FirstName + " " + teacherUser.LastName,
                });
            }

            return model;
        }

        public async Task<List<ReviewViewModel>> GetReviewsBySubjectId(int subjectId)
        {
            var userId = accountService.GetUserId();

            var student = await context.Students.FirstOrDefaultAsync(x => x.UserId == userId);

            var studentId = 0;

            if (student == null)
            {
                var parent = await context.Parents.FirstOrDefaultAsync(x => x.UserId == userId);

                studentId = parent.StudentId;
            }
            else
            {
                studentId = student.Id;
            }

            if (studentId == 0)
            {
                return null;
            }

            return await context.Reviews.Where(x => x.SubjectId == subjectId && x.StudentId == studentId)
                .Select(x => new ReviewViewModel
                {
                    Description = x.Description,
                    AddedOn = x.AddedOn
                }).ToListAsync();
        }
    }
}
