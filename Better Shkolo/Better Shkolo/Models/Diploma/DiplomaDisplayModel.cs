using System.ComponentModel.DataAnnotations;

namespace Better_Shkolo.Models.Diploma
{
    public class DiplomaDisplayModel
    {
        [Display(Name = " Търси по име")]
        public string Name { get; set; } = null!;
        public string DocType { get; set; } = null!;
        public List<DiplomaIndexModel> Diplomas { get; set; } = null!;
    }
}
