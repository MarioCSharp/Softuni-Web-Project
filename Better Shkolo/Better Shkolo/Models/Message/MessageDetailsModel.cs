namespace BetterShkolo.Models.Message
{
    public class MessageDetailsModel
    {
        public string Titile { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string SentByUserName { get; set; } = null!;
        public string SentByUserEmail { get; set; } = null!;
        public bool Deleted { get; set; }
        public string TimeSent { get; set; } = null!;
    }
}
