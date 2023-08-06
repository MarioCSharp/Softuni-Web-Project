using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Better_Shkolo.Data.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; }
        [Required]
        public int GradeId { get; set; }
        [ForeignKey(nameof(GradeId))]
        public Grade Grade { get; set; }
        [Required]
        public int GradeTeacherId { get; set; }
        [ForeignKey(nameof(GradeTeacherId))]
        public Teacher GradeTeacher { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public Parent Parent { get; set; }
        public List<Mark> Marks { get; set; }
        public List<Absences> Absences { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
