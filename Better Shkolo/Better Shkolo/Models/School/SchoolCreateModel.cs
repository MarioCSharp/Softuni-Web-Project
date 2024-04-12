using BetterShkolo.Data.Models;
using BetterShkolo.Data;
using BetterShkolo.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.School
{
    public class SchoolCreateModel
    {
        [Required]
        [StringLength(Constants.School.NameMaxLength, MinimumLength = Constants.School.NameMinLength)]
        [Display(Name = "Име")]
        public string Name { get; set; }
        [Required]
        [StringLength(Constants.School.CityMaxLength, MinimumLength = Constants.School.CityMinLength)]
        [Display(Name = "Град")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Директор")]
        public string DirectorId { get; set; }

        public List<UserDisplayModel> AvailableUsers { get; set; } = null!;
    }
}
