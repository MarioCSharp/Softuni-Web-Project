namespace BetterShkolo.Models.Absences
{
    public class AbsencesViewModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string TeacherName { get; set; } = null!;
        public int TeacherId { get; set; }
    }
}
