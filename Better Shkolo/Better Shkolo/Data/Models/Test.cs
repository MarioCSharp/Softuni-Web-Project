using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Data.Models
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
        public Subject Subject { get; set; }
        [Required]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        [Required]
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; }
    }
}
