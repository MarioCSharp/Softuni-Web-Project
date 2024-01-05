using Better_Shkolo.Data.Enums;
using Better_Shkolo.Models.Account;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.School;
using Better_Shkolo.Models.Teacher;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Message
{
    public class MessageSendModel
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Content { get; set; } = null!;
        [Required]
        public string SentToUserId { get; set; }

        public List<UserDisplayModel> Users { get; set; }
    }
}
