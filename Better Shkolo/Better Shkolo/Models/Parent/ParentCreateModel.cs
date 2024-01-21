using Better_Shkolo.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Parent
{
    public class ParentCreateModel
    {
        [Required]
        [Display(Name = "Родител")]
        public string UserId { get; set; } = null!;
        public List<UserDisplayModel> Users { get; set; }
    }
}
