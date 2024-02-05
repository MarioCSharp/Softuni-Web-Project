using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Account
{
    public class UserAddressModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string Country { get; set; } = null!;
    }
}
