using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Test
{
    public class TestAddModel
    {
        [Required]
        [Display(Name = "Дата")]
        public DateTime TestDate { get; set; }

        public int SubjectId { get; set; }

        [Required]
        [Display(Name = "Тип")]
        public string Type { get; set; } = null!;

        [Required]
        [Display(Name = "Час")]
        public int Period { get; set; }
    }
}
