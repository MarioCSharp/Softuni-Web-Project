using Better_Shkolo.Data;
using Better_Shkolo.Models.Teacher;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Grade
{
    public class GradeCreateModel
    {
        [Required]
        [StringLength(Constants.Grade.GradeNameMaxLength, MinimumLength = Constants.Grade.GradeNameMinLength)]
        [Display(Name = "Grade Name")]
        public string GradeName { get; set; }
        [Required]
        [StringLength(Constants.Grade.GradeSpecialtyMaxLength, MinimumLength = Constants.Grade.GradeSpecialtyMinLength)]
        [Display(Name = "Grade Specialty")]
        public string GradeSpecialty { get; set; }
        [Required]
        [Display(Name = "Teacher")]
        public int TeacherId { get; set; }
        [Required]
        public int SchoolId { get; set; }

        public List<TeacherDisplayModel> Teachers { get; set; }
    }
}
