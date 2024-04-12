using AutoMapper;
using BetterShkolo.Data;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.Review;
using BetterShkolo.Services.AccountService;
using BetterShkolo.Services.TeacherService;
using Microsoft.EntityFrameworkCore;

namespace BetterShkolo.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private ApplicationDbContext context;
        private ITeacherService teacherService;
        private IAccountService accountService;
        private IMapper mapper;
        public ReviewService(ApplicationDbContext context,
                             ITeacherService teacherService,
                             IAccountService accountService,
                             IMapper mapper)
        {
            this.context = context;
            this.teacherService = teacherService;
            this.accountService = accountService;
            this.mapper = mapper;
        }
        public async Task<bool> Add(ReviewAddModel model)
        {
            var teacher = await teacherService.GetTeacher();
            var subject = await context.Subjects.FindAsync(model.SubjectId);
            var student = await context.Students.FindAsync(model.StudentId);

            if (teacher is null)
            {
                return false;
            }

            var school = await context.Schools.FindAsync(teacher.SchoolId);

            if (subject is null || student is null || school is null)
            {
                return false;
            }

            var review = mapper.Map<Review>(model);
            review.AddedOn = DateTime.Now;
            review.TeacherId = teacher.Id;
            review.SchoolId = teacher.SchoolId;
            review.Term = model.Term;

            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();

            return await context.Reviews.ContainsAsync(review);
        }

        public async Task<List<ReviewDisplayModel>> GetReviews(string userId = null)
        {
            if (userId is null)
            {
                userId = accountService.GetUserId();
            }

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
                        StudentId = studentId,
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
                    StudentId = review.StudentId,
                    TeacherName = teacherUser.FirstName + " " + teacherUser.LastName,
                });
            }

            return model;
        }

        public async Task<List<ReviewViewModel>> GetReviewsBySubjectId(int subjectId, string userId)
        {
            if (userId is null)
            {
                userId = accountService.GetUserId();
            }

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
