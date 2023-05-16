using Better_Shkolo.Data;
using Better_Shkolo.Models.Teacher;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Grade
{
    public class GradeCreateModel
    {
        [Required]
        [StringLength(Constants.Grade.GradeNameMaxLength, MinimumLength = Constants.Grade.GradeNameMinLength)]
        public string GradeName { get; set; }
        [Required]
        [StringLength(Constants.Grade.GradeSpecialtyMaxLength, MinimumLength = Constants.Grade.GradeSpecialtyMinLength)]
        public string GradeSpecialty { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int SchoolId { get; set; }

        public List<TeacherDisplayModel> Teachers { get; set; }
    }
}
