namespace BetterShkolo.Models.Table
{
    public class TableDisplayModel
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public int Period { get; set; }
        public int Day { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = null!;
    }
}
