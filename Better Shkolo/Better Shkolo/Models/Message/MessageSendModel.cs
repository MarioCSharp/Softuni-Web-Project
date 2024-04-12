using BetterShkolo.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Message
{
    public class MessageSendModel
    {
        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; } = null!;
        [Required]
        [Display(Name = "Съобщение")]
        public string Content { get; set; } = null!;
        [Required]
        public string SentToUserId { get; set; }

        public List<UserDisplayModel> Users { get; set; }
    }
}
