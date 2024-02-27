using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Better_Shkolo.Data.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(Constants.Grade.GradeNameMaxLength, MinimumLength = Constants.Grade.GradeNameMinLength)]
        public string GradeName { get; set; }
        [Required]
        [StringLength(Constants.Grade.GradeSpecialtyMaxLength, MinimumLength = Constants.Grade.GradeSpecialtyMinLength)]
        public string GradeSpecialty { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; }

        public List<Student> Students { get; set; }
    }
}
