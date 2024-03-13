namespace Better_Shkolo.Models.Document
{
    public class DocumentIndexModel
    {
        public string Name { get; set; } = null!;
        public byte[] File { get; set; } = null!;
        public int DocumentId { get; set; }
        public int SchoolId { get; set; }
    }
}
