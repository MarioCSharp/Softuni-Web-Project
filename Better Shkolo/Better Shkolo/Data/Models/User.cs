using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(Constants.User.FirstNameMaxLength, MinimumLength = Constants.User.FirstNameMinLength)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(Constants.User.LastNameMaxLength, MinimumLength = Constants.User.LastNameMinLength)]
        public string LastName { get; set; }
    }
}
