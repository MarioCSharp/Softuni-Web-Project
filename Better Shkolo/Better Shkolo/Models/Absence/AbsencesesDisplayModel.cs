using Better_Shkolo.Models.Mark;

namespace Better_Shkolo.Models.Absence
{
    public class AbsencesesDisplayModel
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public List<AbsencesViewModel> Absenceses { get; set; }
    }
}
