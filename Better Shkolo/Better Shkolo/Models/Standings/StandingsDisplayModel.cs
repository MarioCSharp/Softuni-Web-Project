using Better_Shkolo.Models.Student;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Standings
{
    public class StandingsDisplayModel
    {
        public List<StudentStandingsModel> SchoolStandings { get; set; }
        [Display(Name = "Name")]
        public string SearchTerm { get; set; }
    }
}
