using BetterShkolo.Models.Mark;

namespace BetterShkolo.Models.Absences
{
    public class AbsencesesDisplayModel
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public List<AbsencesViewModel> Absenceses { get; set; }
    }
}
