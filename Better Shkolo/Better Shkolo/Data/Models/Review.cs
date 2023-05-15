using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Data.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        [StringLength(Constants.Review.DescriptionMaxLength, MinimumLength = Constants.Review.DescriptionMinLength)]
        public string Description { get; set; }
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
