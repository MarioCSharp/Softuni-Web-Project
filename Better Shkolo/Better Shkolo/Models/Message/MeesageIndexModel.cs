using Better_Shkolo.Data.Models;

namespace Better_Shkolo.Models.Message
{
    public class MeesageIndexModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string SentByUserId { get; set; } = null!;
        public bool Read { get; set; }
        public string SentByFirstName { get; set; } = null!;
        public string SentByLastName { get; set; } = null!;
        public string SentByEmail { get; set; } = null!;
    }
}
