using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Erasmus
{
    public class ErasmusApplicationEvaluationModel
    {
        [Required]
        [Display(Name = "Точки")]
        public int Points { get; set; }
        public int ApplicationId { get; set; }
    }
}
