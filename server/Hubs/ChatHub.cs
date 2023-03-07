using Microsoft.AspNetCore.SignalR;
using server.Hubs.Clients;
using server.Models;

namespace server.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage(message);
        }
    }
}