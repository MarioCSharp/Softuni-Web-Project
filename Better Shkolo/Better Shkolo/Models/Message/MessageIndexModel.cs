namespace Better_Shkolo.Models.Message
{
    public class MessageIndexModel
    {
        public List<RecievedMessageModel> Recieved { get; set; } = null!;
        public List<SentMessageModel> Sent { get; set; } = null!;
        public List<DeleteMessageModel> Deleted { get; set; } = null!;
    }
}
