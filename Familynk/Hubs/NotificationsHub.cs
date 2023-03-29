using Microsoft.AspNetCore.SignalR;

namespace Familynk.Hubs;
[Authorize]
public class NotificationsHub : Hub<INotificationClient>
{
    //public FamilyMember CurrentUser { get; set; }
    /// <summary>
    /// this method is called when an incoming notification is meant to be received by all Members of a Family
    /// </summary>
    /// <param name="user">user name of recipient</param>
    /// <param name="message">the message contained in the <see cref="Notification"/> notification.</param>
    /// <returns></returns>
    public async Task NotifyAll(int familyUnitId, string message, FamilyContext context, Hub<INotificationClient> notificationsHub)
    {
        var family = await context.Neighborhood.FindAsync(familyUnitId);
        await notificationsHub.Clients.Groups(family!.FamilyName).RecieveNotificationAsync(familyUnitId, message);
    }
    public async Task NotifyCaller(int familyUnitId, string message) =>
        await Clients.Caller.RecieveNotificationAsync(familyUnitId, message);

    public async Task NotifyGroup(string familyName, string message, FamilyContext context)
    {
        var family = await context.Neighborhood.FirstAsync(f => f.FamilyName.Equals(familyName));
        await Clients.Group(familyName).RecieveNotificationAsync(family.FamilyUnitId, message);
    }

    public override async Task OnConnectedAsync()
    {
        
        await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.);
        await base.OnConnectedAsync();
    }
}

