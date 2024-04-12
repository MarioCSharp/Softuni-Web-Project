using BetterShkolo.Models.Lesson;

namespace BetterShkolo.Services.LessonService
{
    public interface ILessonService
    {
        Task<bool> AddAsync(LessonAddModel model);
        Task<List<LessonViewModel>> GetLessons(int subjectId);
        Task<LessonDisplayModel> GetLesson(int lessonId);
        Task<LessonSubjectModel> GetSubjects();
    }
}
