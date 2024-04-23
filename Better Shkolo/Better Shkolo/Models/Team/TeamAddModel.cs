using BetterShkolo.Models.Grade;
using BetterShkolo.Models.Teacher;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Team
{
    public class TeamAddModel
    {
        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; } = null!;
        [Required]
        [Display(Name = "Клас")]
        public int GradeId { get; set; }
        [Required]
        [Display(Name = "Учител")]
        public int TeacherId { get; set; }

        public List<TeacherDisplayModel> Teachers { get; set; } = new List<TeacherDisplayModel>();
        public List<GradeDisplayModel> Grades { get; set; } = new List<GradeDisplayModel>();
    }
}
