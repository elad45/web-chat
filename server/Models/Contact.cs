namespace server.Models
{
    public class Contact
    {
        public Contact( string id, string name, string server)
        {
            Id = id;
            Name = name;
            Server = server;
            // last message in the conversation with that certain contact
            Last = null;
            // the time the last message has been sent
            Lastdate = null;
        }

        //    public string UserId { get; set; }
        //this is contact id
        public string Id { get; set; }


        public string Name { get; set; }

        public string Server { get; set; }

        public string Last { get; set; }

        public DateTime? Lastdate { get; set; }
    }
}