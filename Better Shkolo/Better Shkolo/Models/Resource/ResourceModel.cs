using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Resource
{
    public class ResourceModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Име на ресурс")]
        public string Name { get; set; } = null!;
        public int LessonId { get; set; }
        [Display(Name = "Линк")]
        public string Link { get; set; } = null!;
        [Display(Name = "Файл")]
        public byte[] File { get; set; } = null!;
    }
}
