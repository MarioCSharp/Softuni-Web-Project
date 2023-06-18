namespace Better_Shkolo.Models.Api
{
    public class StatisticsDisplayModel
    {
        public double Success { get; set; }
        public int Absenceses { get; set; }
        public int Reviews { get; set; }
        public int Tests { get; set; }

        public int PlaceInClass { get; set; }
        public int PlaceInGrade { get; set; }
        public int PlaceInSchool { get; set; }
    }
}
