using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Director
{
    public class EmailSendModel
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        public int SchoolId { get; set; }
    }
}
