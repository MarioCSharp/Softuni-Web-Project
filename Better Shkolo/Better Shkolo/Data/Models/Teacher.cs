using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Better_Shkolo.Data.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; }
        public School School { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public List<Student> Students { get; set; }
    }
}
