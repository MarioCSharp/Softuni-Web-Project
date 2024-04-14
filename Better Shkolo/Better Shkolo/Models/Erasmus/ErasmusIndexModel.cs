using Better_Shkolo.Models.Erasmus;
using BetterShkolo.Models.Student;

namespace BetterShkolo.Models.Erasmus
{
    public class ErasmusIndexModel
    {
        public ErasmusIndexModel()
        {
        }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; } = null!;
        public bool Active { get; set; }
        public int CurrentPage { get; set; } // Current page number
        public int TotalPages { get; set; }
        public int StudentsPerPage { get; set; } // Number of students per page
        private List<StudentDisplayModel> GetDisplayedStudents()
        {
            int startIndex = (CurrentPage - 1) * StudentsPerPage;
            int endIndex = Math.Min(startIndex + StudentsPerPage, AligibleStudents.Count);
            return AligibleStudents.GetRange(startIndex, endIndex - startIndex);
        }
        public List<StudentDisplayModel> AligibleStudents { get; set; } = null!;
        public List<ErasmusDocumentIndexModel> Documents{ get; set; } = null!;
    }
}
