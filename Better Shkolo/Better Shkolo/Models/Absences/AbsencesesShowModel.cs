namespace BetterShkolo.Models.Absences
{
    public class AbsencesesShowModel
    {
        public int Id { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime? ExcusedOn { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
    }
}
