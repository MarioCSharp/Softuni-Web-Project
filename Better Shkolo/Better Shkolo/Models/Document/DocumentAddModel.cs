using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Document
{
    public class DocumentAddModel
    {
        [Required]
        [Display(Name = "Име на документ")]
        public string Name { get; set; } = null!;
        [Display(Name = "Файл")]
        public byte[] File { get; set; } = null!;
    }
}
