namespace Familynk.Controllers;

public class HouseController : Controller
{
    private readonly SignInManager<FamilyMember> _signInManager;
    private readonly UserManager<FamilyMember> _userManager;
    private readonly INotyfService _toast;

    public HouseController(IServiceProvider services)
    {
        _signInManager = services.GetRequiredService<SignInManager<FamilyMember>>();
        _userManager = _signInManager.UserManager;
        _toast = services.GetRequiredService<INotyfService>();
    }

    public async Task<IActionResult> FrontPorch()
    {
        await Task.CompletedTask;
        return View();
    }

    public async Task<IActionResult> BackYard()
    {
        await Task.CompletedTask;
        return View();
    }

    public async Task<IActionResult> Kitchen()
    {
        await Task.CompletedTask;
        return View();
    }

    public async Task<IActionResult> Refrigerator()
    {
        await Task.CompletedTask;
        return View();
    }

    public async Task<IActionResult> BulletinBoard()
    {
        await Task.CompletedTask;
        return View();
    }

    public async Task<IActionResult> Calendar()
    {
        await Task.CompletedTask;
        return View();
    }
}

