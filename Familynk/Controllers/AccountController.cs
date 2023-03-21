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
            UserProfileVM uvm = new(CurrentUser);
            return View(uvm);
        }
        [HttpGet]
        public IActionResult Register()
        {
            RegisterVM rvm = new();
            return View(rvm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM rvm)
        {
            if (!ModelState.IsValid)
            {
                _toast.Error("something went wrong");
                return View(rvm);
            }
            
            var userManager = _signinManager.UserManager;
            var result = await userManager.CreateAsync(new FamilyMember()
            {
                Name = rvm.Name,
                UserName = rvm.UserName,
                Email = rvm.Email,
            }, rvm.Password);
            if (result.Succeeded)
            {
                _toast.Success("Registered new Family member\n" + rvm.Name);
                return RedirectToAction("Index","Home");
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
                var result = await _signinManager.PasswordSignInAsync(lvm.UserName, lvm.Password, isPersistent: lvm.RememberMe, lockoutOnFailure: false);
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
        public async Task<IActionResult> ChangeUserInfo(UserProfileVM uvm)
        {
            var user = await _signinManager.UserManager.FindByIdAsync(uvm.Id);
            if (uvm is null || user is null)
            {
                _toast.Error("I unno what hpppund");
                return RedirectToAction("Index");
            }
            if (uvm.UserName != "No UserName" && uvm.UserName != user.UserName)
            {
                user.UserName = uvm.UserName;
                user.NormalizedUserName = uvm.UserName.ToUpper();
            }
            if (uvm.Email is not null && !uvm.Email.Equals(user!.Email))
            {
                user.Email = uvm.Email;
                user.NormalizedEmail = uvm.Email.ToUpper();
            }
            if (uvm.Name is not null && !uvm.Name.Equals(user.Name))
            {
                user.Name = uvm.Name;
            }
            if (uvm.Birthday != user.Birthday)
            {
                user.Birthday = uvm.Birthday;
            }
            await _signinManager.UserManager.UpdateAsync(user!);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserProfileVM uvm)
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
            var result = await _signinManager.UserManager.ChangePasswordAsync(currentUser!, uvm.Password, uvm.NewPassword);
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

