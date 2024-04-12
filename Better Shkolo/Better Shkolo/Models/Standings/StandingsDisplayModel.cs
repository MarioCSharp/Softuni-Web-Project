using BetterShkolo.Models.Student;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Standings
{
    public class StandingsDisplayModel
    {
        public List<StudentStandingsModel> SchoolStandings { get; set; }
        [Display(Name = "Име")]
        public string SearchTerm { get; set; }

        public int PlaceGrade { get; set; }
        public int PlaceYear { get; set; }
        public int PlaceSchool { get; set; }
    }
}
