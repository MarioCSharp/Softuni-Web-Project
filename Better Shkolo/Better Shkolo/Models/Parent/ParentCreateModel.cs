using Better_Shkolo.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Parent
{
    public class ParentCreateModel
    {
        [Required]
        [Display(Name = "Parent")]
        public string UserId { get; set; }
        public List<UserDisplayModel> Users { get; set; }
    }
}
