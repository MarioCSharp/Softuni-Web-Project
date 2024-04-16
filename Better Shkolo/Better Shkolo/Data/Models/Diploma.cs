using BetterShkolo.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Better_Shkolo.Data.Models
{
    public class Diploma
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; } = null!;
        [Required]
        public string SchoolYear { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string Identification { get; set; } = null!;
        [Required]
        public string Series { get; set; } = null!;
        [Required]
        public string FabricNumber { get; set; } = null!;
        [Required]
        public int RegistrationNumber { get; set; }
        [Required]
        public int YearRegistrationNumber { get; set; }
        [Required]
        public DateTime IssuedDate { get; set; }
        [Required]
        public string EducationForm { get; set; } = null!;
        [Required]
        public bool Cancelled { get; set; }
        [Required]
        public byte[] File { get; set; } = null!;
        [Required]
        public string FileExtension { get; set; } = null!;
        [Required]
        public int SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = null!;
    }
}
