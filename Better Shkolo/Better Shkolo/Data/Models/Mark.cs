using BetterShkolo.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterShkolo.Data.Models
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
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; }
        [Required]
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; }
        [Required]
        public int Term { get; set; }
        [Required]
        public string Type { get; set; } = null!;
    }
}
