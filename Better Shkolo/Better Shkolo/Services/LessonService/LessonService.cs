using BetterShkolo.Data.Models;
using BetterShkolo.Models.Resource;
using BetterShkolo.Models.Subject;
using BetterShkolo.Data;
using BetterShkolo.Models.Lesson;
using BetterShkolo.Services.AccountService;
using Microsoft.EntityFrameworkCore;

namespace BetterShkolo.Services.LessonService
{
    public class LessonService : ILessonService
    {
        private ApplicationDbContext context;
        private IAccountService accountService;
        public LessonService(ApplicationDbContext context,
                             IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
        }

        public async Task<bool> AddAsync(LessonAddModel model)
        {
            var s = await context.Subjects.FindAsync(model.SubjectId);

            if (s == null) return false;

            var lsn = new Lesson()
            {
                Name = model.Name,
                SubjectId = s.Id,
                GradeId = s.GradeId
            };

            await context.Lessons.AddAsync(lsn);
            await context.SaveChangesAsync();

            return await context.Lessons.ContainsAsync(lsn);
        }

        public async Task<LessonDisplayModel> GetLesson(int lessonId)
        {
            var lsn = await context.Lessons.FindAsync(lessonId);

            if (lsn is null) return null;

            var mdl = new LessonDisplayModel()
            {
                Id = lsn.Id,
                Name = lsn.Name,
                SubjectId = lsn.SubjectId,
                Resources = await context.Resources
                .Where(x => x.LessonId == lessonId)
                .Select(x => new ResourceModel()
                {
                    Id = x.Id,
                    LessonId = x.LessonId,
                    Name = x.Name,
                    Link = x.Link,
                    File = x.File
                }).ToListAsync()
            };

            return mdl;
        }

        public async Task<List<LessonViewModel>> GetLessons(int subjectId)
        {
            return await context.Lessons
                .Where(x => x.SubjectId == subjectId)
                .Select(x => new LessonViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                }).ToListAsync();
        }

        public async Task<LessonSubjectModel> GetSubjects()
        {
            var uId = accountService.GetUserId();

            var s = await context.Students.FirstOrDefaultAsync(x => x.UserId == uId);

            return new LessonSubjectModel
            {
                Subjects = await context.Subjects
                .Where(x => x.GradeId == s.GradeId)
                .Select(x => new SubjectModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync()
            };
        }
    }
}
