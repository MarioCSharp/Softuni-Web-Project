namespace Better_Shkolo.Models.Message
{
    public class DeleteMessageModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string SentByFirstName { get; set; } = null!;
        public string SentByLastName { get; set; } = null!;
        public string SentToEmail { get; set; } = null!;
        public bool Deleted { get; set; }
        public string TimeSent { get; set; } = null!;
    }
}
