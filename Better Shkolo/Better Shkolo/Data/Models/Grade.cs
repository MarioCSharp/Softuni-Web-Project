using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Data.Models
{
    public class Grade
    {
        public int Id { get; set; }
        [Required]
        [StringLength(Constants.Grade.GradeNameMaxLength, MinimumLength = Constants.Grade.GradeNameMinLength)]
        public string GradeName { get; set; }
        [Required]
        [StringLength(Constants.Grade.GradeSpecialtyMaxLength, MinimumLength = Constants.Grade.GradeSpecialtyMinLength)]
        public string GradeSpecialty { get; set; }
        [Required]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; }

        public List<Student> Students { get; set; }
    }
}
