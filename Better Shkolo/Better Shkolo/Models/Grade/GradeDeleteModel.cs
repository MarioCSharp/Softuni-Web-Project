using BetterShkolo.Models.Subject;
using BetterShkolo.Data;
using BetterShkolo.Models.Teacher;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Grade
{
    public class GradeDeleteModel
    {
        [Required]
        [StringLength(Constants.Grade.GradeNameMaxLength, MinimumLength = Constants.Grade.GradeNameMinLength)]
        public string GradeName { get; set; } = null!;
        [Required]
        [StringLength(Constants.Grade.GradeSpecialtyMaxLength, MinimumLength = Constants.Grade.GradeSpecialtyMinLength)]
        public string GradeSpecialty { get; set; } = null!;
        [Required]
        [Display(Name = "Учител")]
        public int TeacherId { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [Required]
        public int OldTeacherId { get; set; }

        public List<TeacherDisplayModel> Teachers { get; set; }
    }
}
