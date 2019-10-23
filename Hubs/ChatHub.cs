using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MessagingServer.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IHandlerFacade handlers;
        public ChatHub(IHandlerFacade handlers)
        {
            this.handlers = handlers;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendMessageToGroup(string user, string message, string groupName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }

        public async Task ConnectToGroup(string connectionId, string groupName)
        {
            await Groups.AddToGroupAsync(connectionId, groupName);
        }

        public async Task DisconnectToGroup(string connectionId, string groupName)
        {
            await Groups.RemoveFromGroupAsync(connectionId, groupName);
        }
    }
}