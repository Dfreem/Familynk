

namespace Familynk.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IServiceProvider _services;
    private readonly FamilyContext _context;
    private readonly SignInManager<FamilyMember> _signInManager;
    private readonly UserManager<FamilyMember> _userManager;
    public FamilyMember CurrentUser { get; set; }

    public HomeController(ILogger<HomeController> logger, IServiceProvider services, FamilyContext context)
    {
        _logger = logger;
        _services = services;
        _context = context;
        _signInManager = services.GetRequiredService<SignInManager<FamilyMember>>();
        _userManager = _signInManager.UserManager;
        string? uName = _signInManager.Context.User!.Identity!.Name;

        CurrentUser = _userManager.FindByNameAsync(uName!).Result! ?? new() { Name = "Guest" };
    }
    // Home/Index shows a view in which a user may see all the registered users on the platrform.
    // A user is able to do the following within this view:
    /*
     - Search for a family by family name or a members user name
     - Click on a family to navigatew to their front porch
     - Create a family and add themselves to it
     */

    [AllowAnonymous]
    public IActionResult Index()
    {
        WelcomeVM wvm = new()
        {
            Neighborhood = _context.Neighborhood
                .Include(n => n.Members).ToList(),
            Visitor = CurrentUser
        };
        return View(wvm);
    }

    // GET: /<controller>/
    public IActionResult FamilyRoom()
    {
        var messages = _context.ChatTv.Where(m => m.FamilyUnitId.Equals(CurrentUser.FamilyUnitId));

        FamilyChat familyMessages = new()
        {
            Messages = messages.ToList(),
            SenderId = CurrentUser.Id
        };
        LivingRoomVM lvm = new()
        {
            ChatTv = familyMessages,
            CurrentUser = CurrentUser,
            Family = _context.Neighborhood.First(
                u => u.FamilyUnitId.Equals(CurrentUser.FamilyUnitId))
        };
        lvm.Family.GetCalendar = new()
        {
            FamilyName = lvm.Family.FamilyName
        };
        return View(lvm);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

