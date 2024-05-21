using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterShkolo.Data.Models
{
    public class Test
    {
        public int Id { get; set; }
        [Required]
        public DateTime AddedOn { get; set; }
        [Required]
        public DateTime TestDate { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = null!;
        [Required]
        public int TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; } = null!;
        [Required]
        public int GradeId { get; set; }
        [ForeignKey(nameof(GradeId))]
        public Grade Grade { get; set; } = null!;
        [Required]
        public int SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;
        [Required]
        public int Period { get; set; }
        [Required]
        public int Term { get; set; }
        public bool NotificationSend { get; set; }
    }
}
