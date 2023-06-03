namespace Better_Shkolo.Models.Review
{
    public class ReviewDisplayModel
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }
}
