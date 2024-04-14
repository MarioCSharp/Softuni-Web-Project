using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Erasmus
{
    public class ErasmusDocumentAddModel
    {
        [Required]
        [Display(Name = "Име на документа")]
        public string Name { get; set; } = null!;
        public byte[] File { get; set; } = null!;
    }
}
