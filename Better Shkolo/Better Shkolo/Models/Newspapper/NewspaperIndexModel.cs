namespace Better_Shkolo.Models.Newspapper
{
    public class NewspaperIndexModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Date { get; set; } = null!;
    }
}
