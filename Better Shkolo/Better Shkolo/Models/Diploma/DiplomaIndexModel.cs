namespace Better_Shkolo.Models.Diploma
{
    public class DiplomaIndexModel
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string SchoolYear { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Identification { get; set; } = null!;
        public string Series { get; set; } = null!;
        public string FabricNumber { get; set; } = null!;
        public int RegistrationNumber { get; set; }
        public int YearRegistrationNumber { get; set; }
        public DateTime IssuedDate { get; set; }
        public string EducationForm { get; set; } = null!;
        public bool Cancelled { get; set; }
    }
}
