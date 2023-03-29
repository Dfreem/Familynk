using Microsoft.AspNetCore.Identity;

namespace Familynk.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServiceProvider _services;
        private readonly SignInManager<FamilyMember> _signinManager;
        private readonly INotyfService _toast;

        public FamilyMember CurrentUser { get; set; }

        public AccountController(IServiceProvider services)
        {
            _services = services;
            _signinManager = services.GetRequiredService<SignInManager<FamilyMember>>();

            var userManager = _signinManager.UserManager;
            string? userName = _signinManager.Context.User!.Identity!.Name;

            _toast = services.GetRequiredService<INotyfService>();

            if (!userName.IsNullOrEmpty())
            { CurrentUser = userManager.FindByNameAsync(userName!).Result!; }
            else { CurrentUser = new() { Name = "new user" }; }
        }

        [Authorize]
        public IActionResult Index()
        {
            AccountVM avm = new(CurrentUser);
            return View(avm);
        }
        [HttpGet]
        public IActionResult Register()
        {
            AccountVM avm = new();
            return View(avm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountVM avm)
        {
            if (!ModelState.IsValid)
            {
                _toast.Error("something went wrong");
                return View(avm);
            }
            
            var userManager = _signinManager.UserManager;
            var result = await userManager.CreateAsync(new FamilyMember()
            {
                Name = avm.Name,
                UserName = avm.UserName,
                Email = avm.Email,
            }, avm.CurrentPassword);
            if (result.Succeeded)
            {
                _toast.Success("Registered new Family member\n" + avm.Name);
                return RedirectToAction("Index","Home");
            }
            _toast.Error("Unable to Register User" + result.Errors.Humanize());
            return View(avm);
        }
        public IActionResult Login()
        {
            AccountVM lvm = new();
            return View(lvm);
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountVM lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(lvm.UserName!, lvm.CurrentPassword, isPersistent: lvm.RememberMe, lockoutOnFailure: false);
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
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LogOutAsync()
        {
            await _signinManager.SignOutAsync();
            _toast.Success("You are now signed out, Goodbye!");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserInfo(AccountVM avm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(avm.ReturnUrl); 
            }
            var user = await _signinManager.UserManager.FindByIdAsync(avm.CurrentUserId!);
            if (avm is null || user is null)
            {
                _toast.Error("I unno what hpppund");
                return RedirectToAction("Index");
            }
            if (avm.UserName !is not null && avm.UserName != user.UserName)
            {
                user.UserName = avm.UserName;
                user.NormalizedUserName = avm.UserName.ToUpper();
            }
            if (avm.Email is not null && !avm.Email.Equals(user!.Email))
            {
                user.Email = avm.Email;
                user.NormalizedEmail = avm.Email.ToUpper();
            }
            if (avm.Name is not null && !avm.Name.Equals(user.Name))
            {
                user.Name = avm.Name;
            }
        
            await _signinManager.UserManager.UpdateAsync(user!);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(AccountVM uvm)
        {
            if (!ModelState.IsValid)
            {
                _toast.Error("could not change password");
                return RedirectToAction("Index");
            }

            // Check to see that the same Password was entered twice.
            if (!uvm.NewPassword.Equals(uvm.ConfirmPassword)) return RedirectToAction("Index");
            // get userName that is currently signed in
            string? currentUserName = _signinManager.Context.User.Identity!.Name;
            var currentUser = await _signinManager.UserManager.FindByNameAsync(currentUserName!);

            //UserManager checks the current password first before changing to the new one.
            var result = await _signinManager.UserManager.ChangePasswordAsync(currentUser!, uvm.CurrentPassword, uvm.NewPassword);
            if (result.Succeeded)
            {
                _toast.Success("Your password has successfully been changed.");
                return RedirectToAction("Index");
            }
            _toast.Error("unsuccessful" + result.Errors);
            return RedirectToAction("Index");
        }
    }
}

