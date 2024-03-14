namespace Better_Shkolo.Models.Resource
{
    public class ResourceModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int LessonId { get; set; }
        public string Link { get; set; } = null!;
        public byte[] File { get; set; } = null!;
    }
}
