using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Better_Shkolo.Data.Models
{
    public class Absences
    {
        public int Id { get; set; }
        [Required]
        public DateTime AddedOn { get; set; }
        public DateTime? ExcusedOn { get; set; }
        [Required]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        [Required]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        [Required]
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Student Student { get; set; }
        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; }
    }
}
