using BetterShkolo.Models.Grade;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Consultation
{
    public class ConsultationCreateModel
    {
        [Required]
        [Display(Name = "Клас")]
        public int GradeId { get; set; }
        [Required]
        [Display(Name = "Срок")]
        public int Term { get; set; }
        [Required]
        [Display(Name = "Тип")]
        public string Type { get; set; } = null!;

        public List<GradeDisplayModel> Grades { get; set; } = null!;
    }
}
