using BetterShkolo.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Parent
{
    public class ParentCreateModel
    {
        [Required]
        [Display(Name = "Родител")]
        public string UserId { get; set; } = null!;
        public List<UserDisplayModel> Users { get; set; }
    }
}
