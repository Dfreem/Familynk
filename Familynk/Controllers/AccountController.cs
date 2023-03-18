using Microsoft.AspNetCore.Identity;

namespace Familynk.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServiceProvider _services;
        private readonly SignInManager<FamilyMember> _signInManager;
        private readonly INotyfService _toast;

        public FamilyMember CurrentUser { get; set; }

        public AccountController(IServiceProvider services)
        {
            _services = services;
            _signInManager = services.GetRequiredService<SignInManager<FamilyMember>>();

            var userManager = _signInManager.UserManager;
            string? userName = _signInManager.Context.User!.Identity!.Name;

            _toast = services.GetRequiredService<INotyfService>();

            if (!userName.IsNullOrEmpty())
            { CurrentUser = userManager.FindByNameAsync(userName!).Result!; }
            else { CurrentUser = new() { Name = "new user" }; }
        }

        [Authorize]
        public IActionResult Index()
        {
            UserProfileVM uvm = new(CurrentUser);
            return View(uvm);
        }
        [HttpGet]
        public IActionResult Register()
        {
            UserProfileVM uvm = new();
            return View(uvm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserProfileVM rvm)
        {
            if (!ModelState.IsValid)
            {
                _toast.Error("something went wrong");
            }
            var userManager = _signInManager.UserManager;
            var result = await userManager.CreateAsync(new FamilyMember()
            {
                Name = rvm.Name,
                UserName = rvm.UserName,
                Email = rvm.Email,
                Birthday = rvm.Birthday,
            }, "!BassCase987");
            if (result.Succeeded)
            {
                _toast.Success("Registered new Family member\n" + rvm.Name);
            }
            _toast.Error("Unable to Register User" + result.Errors.Humanize());
            return View(rvm);
        }
        public IActionResult Login()
        {
            LoginVM lvm = new();
            return View(lvm);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(lvm.UserName, lvm.Password, isPersistent: lvm.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _toast.Success("Successfully Logged in as " + lvm.UserName);
                    if (!string.IsNullOrEmpty(lvm.ReturnUrl) && Url.IsLocalUrl(lvm.ReturnUrl))
                    { return Redirect(lvm.ReturnUrl); }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                _toast.Error("Unable to Sign in\n" + result.ToString());
            }
            ModelState.AddModelError("", "Invalid username/password.");
            return View(lvm);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

