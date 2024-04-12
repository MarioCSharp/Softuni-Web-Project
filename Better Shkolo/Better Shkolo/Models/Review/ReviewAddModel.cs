using BetterShkolo.Data;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Review
{
    public class ReviewAddModel
    {
        [Required]
        [StringLength(Constants.Review.DescriptionMaxLength, MinimumLength = Constants.Review.DescriptionMinLength)]
        [Display(Name = "Отзив")]
        public string Description { get; set; } = null!;
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        [Display(Name = "Срок")]
        public int Term { get; set; }
    }
}
