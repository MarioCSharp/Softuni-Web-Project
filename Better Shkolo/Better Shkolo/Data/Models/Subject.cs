using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Data.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required]
        [StringLength(Constants.Subject.NameMaxLength, MinimumLength = Constants.Subject.NameMinLength)]
        public string Name { get; set; }
        [Required]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; }
        [Required]
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}
