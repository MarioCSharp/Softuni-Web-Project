namespace Better_Shkolo.Models.Account
{
    public class HomeModel
    {
        public string UserId { get; set; } = null!;
        public int GradeId { get; set; }
        public int PlaceInSchool { get; set; }
        public int PlaceInGrade { get; set; }
        public int PlaceInYear { get; set; }
    }
}
