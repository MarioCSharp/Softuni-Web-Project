using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Account
{
    public class StatusEditModel
    {
        [Required]
        [Display(Name = "Здравен статус")]
        public string Status { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
