namespace server.Models
{
    public interface ITransferService
    {
        public bool Send(string from, string to, string content);
    }
}
