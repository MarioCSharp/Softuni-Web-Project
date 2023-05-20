using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Data.Models
{
    public class Parent
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
