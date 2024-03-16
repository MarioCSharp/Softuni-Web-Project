using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Better_Shkolo.Data.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]    
        public int SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public byte[] File { get; set; } = null!;
        [Required]
        public DateTime AddedOn { get; set; }
        [Required]
        public string FileExtension { get; set; } = null!;
    }
}
