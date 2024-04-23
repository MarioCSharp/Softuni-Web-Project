using BetterShkolo.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Better_Shkolo.Data.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int GradeId { get; set; }
        [ForeignKey(nameof(GradeId))]
        public Grade Grade { get; set; } = null!;
        [Required]
        public int TeacherId { get; set; }
        [ForeignKey(nameof(GradeId))]
        public Teacher Teacher { get; set; } = null!;
        [Required]
        public string RoomId { get; set; } = null!;
    }
}
