using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Better_Shkolo.Data.Models
{
    public class School
    {
        public int Id { get; set; }
        [Required]
        [StringLength(Constants.School.NameMaxLength, MinimumLength = Constants.School.NameMinLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(Constants.School.CityMaxLength, MinimumLength = Constants.School.CityMinLength)]
        public string City { get; set; }
        [Required]
        public string DirectorId { get; set; }
        [ForeignKey(nameof(DirectorId))]
        public User Director { get; set; }
        public List<Grade> Grades { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
    }
}
