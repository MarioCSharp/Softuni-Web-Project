using Better_Shkolo.Models.Lesson;

namespace Better_Shkolo.Services.LessonService
{
    public interface ILessonService
    {
        Task<bool> AddAsync(LessonAddModel model);
        Task<List<LessonViewModel>> GetLessons(int subjectId);
        Task<LessonDisplayModel> GetLesson(int lessonId);
    }
}
