using Better_Shkolo.Models.Account;

namespace Better_Shkolo.Models.Teacher
{
    public class TeacherCreateModel
    {
        public string UserId { get; set; }
        public int SchoolId { get; set; }

        public List<UserDisplayModel> Users { get; set; }
    }
}
