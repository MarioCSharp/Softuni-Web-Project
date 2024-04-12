namespace BetterShkolo.Models.Message
{
    public class SentMessageModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string SentToFirstName { get; set; } = null!;
        public string SentToLastName { get; set; } = null!;
        public string SentToEmail { get; set; } = null!;
        public bool Deleted { get; set; }
        public string TimeSent { get; set; } = null!;
    }
}
