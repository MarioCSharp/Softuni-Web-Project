using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Data.Models
{
    public class YearMark
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(2, 6)]
        public int Value { get; set; }
        [Required]
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = null!;
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = null!;
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; } = null!;
    }
}
