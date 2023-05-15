using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.School
{
    public class SchoolCreateModel
    {
        [Required]
        [StringLength(Constants.School.NameMaxLength, MinimumLength = Constants.School.NameMinLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(Constants.School.CityMaxLength, MinimumLength = Constants.School.CityMinLength)]
        public string City { get; set; }
        [Required]
        public string DirectorId { get; set; }

        public List<UserDisplayModel> AvailableUsers { get; set; } = null!;
    }
}
