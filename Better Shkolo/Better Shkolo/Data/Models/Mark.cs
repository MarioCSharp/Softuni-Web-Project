using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Data.Models
{
    public class Mark
    {
        public int Id { get; set; }
        [Required]
        [Range(Constants.Mark.MarkMinValue, Constants.Mark.MarkMaxValue
            , ErrorMessage = "The mark must be in range {0} to {1}")]
        public int Value { get; set; }
        [Required]
        public DateTime AddedOn { get; set; }
        [Required]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        [Required]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }
        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; }
    }
}
