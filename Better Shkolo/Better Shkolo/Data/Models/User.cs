using BetterShkolo.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(Constants.User.FirstNameMaxLength, MinimumLength = Constants.User.FirstNameMinLength)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(Constants.User.LastNameMaxLength, MinimumLength = Constants.User.LastNameMinLength)]
        public string LastName { get; set; }

        //-----Not required-----
        [Required]
        public string Phone { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string Country { get; set; } = null!;
        [Required]
        public string Chronic { get; set; } = null!;
        [Required]
        public string DoctorName { get; set; } = null!;
        [Required]
        public string DoctorPhone { get; set; } = null!;
        [Required]
        public string DoctorAddress { get; set; } = null!;
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
