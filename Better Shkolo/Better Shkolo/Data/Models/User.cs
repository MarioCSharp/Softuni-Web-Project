using Microsoft.AspNetCore.Identity;

namespace Better_Shkolo.Data.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SchoolId { get; set; }
        public School School { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
        public int GradeTeacherId { get; set; }
        public Teacher GradeTeacher { get; set; }
        public List<Mark> Marks { get; set; }
        public List<Absences> Аbsences { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
