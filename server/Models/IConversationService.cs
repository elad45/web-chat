﻿namespace server.Models
{
    public interface IConversationService
    {
        public List<Conversation> GetAll();

        public Conversation Get(int id);

        public void Add(Conversation conversation);

        public void Edit(int id, Conversation conversation);

        public void Delete(int id);

        public List<Message> GetMessages(string user1, string user2);

        public MessageGet GetMsgByIdConverted(string user1, string MsgId,string user);

        public Message GetMsgById(string user1, string MsgId,string user2);

        public void DeleteMsgById(string user1, string MsgId, string user2);

        public int nextConvId();
        public void AddMessage(string contactId, string content, string userId);

        public List<MessageGet> GetMessagesConverted(string user1, string user2);

        public List<MessageGet> convertMessage(List<Message> messages, string userId);
    }
}