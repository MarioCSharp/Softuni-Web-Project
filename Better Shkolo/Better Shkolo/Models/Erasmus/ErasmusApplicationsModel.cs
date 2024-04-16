using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Erasmus
{
    public class ErasmusApplicationsModel
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string FullName { get; set; } = null!;
        public string EGN { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int FileId { get; set; }
        public int Points { get; set; }
    }
}
