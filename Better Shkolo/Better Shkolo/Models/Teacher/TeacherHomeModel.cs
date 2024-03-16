namespace Better_Shkolo.Models.Teacher
{
    public class TeacherHomeModel
    {
        public double Success { get; set; }
        public int Absences { get; set; }
        public int Reviews { get; set; }
        public int Tests { get; set; }
        public (string, double) FirstPlaceSuccess { get; set; }
        public (string, double) SecondPlaceSuccess { get; set; }
        public (string, double) ThirdPlaceSuccess { get; set; }
        public (string, int) FirstPlaceAbsences { get; set; }
        public (string, int) SecondPlaceAbsences { get; set; }
        public (string, int) ThirdPlaceAbsences { get; set; }
    }
}
