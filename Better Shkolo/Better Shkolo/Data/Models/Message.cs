using Better_Shkolo.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Better_Shkolo.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string SentToUserId { get; set; } = null!;
        [ForeignKey(nameof(SentToUserId))]
        public User SentToUser { get; set; } = null!;
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Content { get; set; } = null!;
        [Required]
        public string SentByUserId { get; set; } = null!;
        [ForeignKey(nameof(SentByUserId))]
        public User SentBy { get; set; } = null!;
        public bool Read { get; set; }
    }
}
