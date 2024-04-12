using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Newspapper
{
    public class NewspapperAddModel
    {
        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; } = null!;
        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; } = null!;
        [Display(Name = "Снимка")]
        public byte[] Image { get; set; } = null!;
    }
}
