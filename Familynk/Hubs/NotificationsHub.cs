
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Familynk.Hubs;
[Authorize]
public class NotificationsHub : Hub<INotificationClient>
{
    private readonly FamilyContext _context;

    private readonly SignInManager<FamilyMember> _signInManager;
    private readonly UserManager<FamilyMember> _userManager;

    private readonly IServiceProvider _services;
    private readonly IHubContext<NotificationsHub, INotificationClient> _hubContext;

    public FamilyMember CurrentUser { get; set; }

    public NotificationsHub(IServiceProvider services, IHubContext<NotificationsHub, INotificationClient> hubContext)
    {
        _context = services.GetRequiredService<FamilyContext>();
        _services = services;
        _signInManager = services.GetRequiredService<SignInManager<FamilyMember>>();
        _userManager = _signInManager.UserManager;
        CurrentUser = _userManager.FindByNameAsync(_signInManager.Context.User.Identity!.Name!).Result!;
        _hubContext = hubContext;
    }
    /// <summary>
    /// this method is called when an incoming notification is meant to be received by all Members of a Family
    /// </summary>
    /// <param name="user">user name of recipient</param>
    /// <param name="message">the message contained in the <see cref="Notification"/> notification.</param>
    /// <returns></returns>
    public async Task NotifyAll(int familyUnitId, string message)
    {
        var family = await _context.Neighborhood.FindAsync(familyUnitId);
        await _hubContext.Clients.Groups(family!.FamilyName).RecieveNotificationAsync(familyUnitId, message);
    }
    public async Task NotifyCaller(int familyUnitId, string message) =>
        await Clients.Caller.RecieveNotificationAsync(familyUnitId, message);

    public async Task NotifyGroup(string familyName, string message)
    {
        var family = await _context.Neighborhood.FirstAsync(f => f.FamilyName.Equals(familyName));
        await Clients.Group(familyName).RecieveNotificationAsync(family.FamilyUnitId, message);
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "FamilyMembers");
        await base.OnConnectedAsync();
    }
}

