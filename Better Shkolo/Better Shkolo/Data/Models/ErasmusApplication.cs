using BetterShkolo.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Better_Shkolo.Data.Models
{
    public class ErasmusApplication
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string EGN { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public int SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = null!;
        public int Points { get; set; }

        public byte[] File { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
    }
}
