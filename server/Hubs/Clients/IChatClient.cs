using server.Models;

namespace server.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);
    }
}