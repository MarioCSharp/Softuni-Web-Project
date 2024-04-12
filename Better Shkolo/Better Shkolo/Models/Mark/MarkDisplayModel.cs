namespace BetterShkolo.Models.Mark
{
    public class MarkDisplayModel
    {
        public int SubjectId { get; set; }
        public int MarkId { get; set; }
        public string SubjectName { get; set; }
        public List<MarkViewModel> Marks { get; set; }
    }
}
