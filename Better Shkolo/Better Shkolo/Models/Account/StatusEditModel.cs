using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Account
{
    public class StatusEditModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name = "Здравен статус")]
        public string Status { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
