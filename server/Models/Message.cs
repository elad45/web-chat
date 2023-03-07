namespace server.Models
{
    public class Message
    {
        public Message(int id, string idOfSender, string content, bool sent)
        {
            Id = id;
            Content = content;
            Created = DateTime.Now;
            Sent = sent;

            IdOfSender = idOfSender;
        }

        public int Id { get; set; }

        public string IdOfSender { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public bool Sent { get; set; }

    }
}