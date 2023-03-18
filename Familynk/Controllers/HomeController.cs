

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
        CurrentUser = _userManager.FindByNameAsync(uName!).Result!;
    }

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

