using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Better_Shkolo.Data.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = null!;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Presence { get; set; } = null!;
        [Required]
        public string Location { get; set; } = null!;
        [Required]
        public string TimeZone { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string AddedById { get; set; } = null!;
        [ForeignKey(nameof(AddedById))]
        public User AddedBy { get; set; } = null!;
    }
}
