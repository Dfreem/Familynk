
using Microsoft.AspNetCore.SignalR;

namespace Familynk.Hubs;

public class NotificationHub : Hub<ITaggable>
{
    public async Task SendMessage(string user, string message)
        => await Clients.All.ReceiveMessage(user, message);

    public async Task SendMessageToCaller(string user, string message)
        => await Clients.Caller.ReceiveMessage(user, message);

    public async Task SendMessageToGroup(string user, string message)
        => await Clients.Group("SignalR Users").ReceiveMessage(user, message);

    public async Task SendNotification(string message)
        => await Clients.
        }

