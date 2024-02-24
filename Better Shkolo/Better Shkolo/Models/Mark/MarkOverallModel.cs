namespace Better_Shkolo.Models.Mark
{
    public class MarkOverallModel
    {
        public int Id { get; set; }
        public string SubjectName { get; set; } = null!;
        public string TeacherFullName { get; set; } = null!;
        public int Value { get; set; }
        public DateTime AddedOn { get; set; }
        public int SubjectId { get; set; }
        public int Term { get; set; }
    }
}
