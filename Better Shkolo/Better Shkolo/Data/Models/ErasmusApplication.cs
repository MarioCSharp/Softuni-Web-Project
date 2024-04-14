using System.ComponentModel.DataAnnotations;

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

        public byte[] Declaration { get; set; } = null!;
        public byte[] MotivationalLetter { get; set; } = null!;
        public byte[] Presentation { get; set; } = null!;
    }
}
