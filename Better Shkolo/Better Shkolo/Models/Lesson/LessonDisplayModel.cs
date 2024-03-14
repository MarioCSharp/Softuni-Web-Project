using Better_Shkolo.Models.Resource;

namespace Better_Shkolo.Models.Lesson
{
    public class LessonDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SubjectId { get; set; }
        public List<ResourceModel> Resources { get; set; } = null!;
    }
}
