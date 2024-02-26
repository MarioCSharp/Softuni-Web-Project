using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Consultation
{
    public class ConsultationCreateModel
    {
        public int GradeId { get; set; }
        [Required]
        [Display(Name = "Срок")]
        public int Term { get; set; }
        [Required]
        [Display(Name = "Тип")]
        public string Type { get; set; } = null!;
    }
}
