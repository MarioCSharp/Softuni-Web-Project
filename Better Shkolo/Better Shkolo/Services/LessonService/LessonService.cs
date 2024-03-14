using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Lesson;
using Better_Shkolo.Models.Resource;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.LessonService
{
    public class LessonService : ILessonService
    {
        private ApplicationDbContext context;
        public LessonService(ApplicationDbContext context)
        {
            this.context = context;
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
    }
}
