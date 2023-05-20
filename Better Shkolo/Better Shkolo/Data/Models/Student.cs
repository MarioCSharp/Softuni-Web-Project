using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Data.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int SchoolId { get; set; }
        public School School { get; set; }
        [Required]
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
        [Required]
        public int GradeTeacherId { get; set; }
        public Teacher GradeTeacher { get; set; }
        public int? ParentId { get; set; }
        public Parent Parent { get; set; }
        public List<Mark> Marks { get; set; }
        public List<Absences> Absences { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
