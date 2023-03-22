
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Familynk.Controllers;

public class FamilyController : Controller
{
    private readonly SignInManager<FamilyMember> _signInManager;
    private readonly UserManager<FamilyMember> _userManager;
    private readonly FamilyContext _context;
    private readonly INotyfService _toast;

    public FamilyMember CurrentUser { get; set; }
    public FamilyController(IServiceProvider services)
    {
        _toast = services.GetRequiredService<INotyfService>();
        _context = services.GetRequiredService<FamilyContext>();
        _signInManager = services.GetRequiredService<SignInManager<FamilyMember>>();
        _userManager = _signInManager.UserManager;
        string? userName = _signInManager.Context.User!.Identity!.Name;
        CurrentUser = _userManager.FindByNameAsync(userName!).Result!;
    }
    // GET: /<controller>/
    public IActionResult FamilyRoom()
    {
        var messages = _context.ChatTv.Where(m => m.FamilyUnitId.Equals(CurrentUser.FamilyUnitId));

        FamilyChat familyMessages = new() { Messages = messages.ToList() };
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
    public IActionResult ScrapBook()
    {
        return View();
    }
    public IActionResult RecipeBook()
    {
        return View();
    }
    public IActionResult Office()
    {
        return View();
    }
    public IActionResult FamilyTree()
    {
        return View();
    }
    public IActionResult FamilyHistory()
    {
        return View();
    }

    #region Non-View Methods

    [HttpPost]
    public async Task<IActionResult> CreateNewFamily(WelcomeVM wvm)
    {
        var family = new FamilyUnit()
        {
            FamilyName = wvm.NewFamily.FamilyName
        };
        _context.Neighborhood.Add(family);
        await _context.SaveChangesAsync();
        family = _context.Neighborhood.First(u => u.FamilyName.Equals(family.FamilyName));
        CurrentUser.FamilyUnitId = family.FamilyUnitId;

        var result = await _userManager.UpdateAsync(CurrentUser);
        if (result.Succeeded)
        {
            _toast.Success($"Succesfully created the {family.FamilyName} Family");
            return RedirectToAction("FamilyRoom", "Family");
        }
        _toast.Error("Unable to create new family");
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> DeleteFamily(int familyId)
    {
        var toDelete = await _context.Neighborhood.FindAsync(familyId);
        if (toDelete is not null)
        {
            _context.Neighborhood.Remove(toDelete);
            _context.SaveChanges();
        }
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> SendFamilyMessage(FamilyMessage newMessage)
    {
        newMessage.SenderId = CurrentUser.Id;
        newMessage.FamilyUnitId = CurrentUser.FamilyUnitId;
        await _context.ChatTv.AddAsync(newMessage);
        if (await _context.ChatTv.Where(m => m.FamilyUnitId.Equals(CurrentUser.FamilyUnitId)).CountAsync() > 25)
        {
            _context.ChatTv.Remove(await _context.ChatTv.LastAsync());
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("FamilyRoom");
    }

    public async Task<IActionResult> DeleteFamilyMessage(int familyMessageId)
    {
        var toDelete = await _context.ChatTv.FindAsync(familyMessageId);
        _context.ChatTv.Remove(toDelete!);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // TODO send invite to family instead of automatically adding them
    [HttpPost]
    public async Task<IActionResult> AddFamilyMember(string toAdd, int fid)
    {
        FamilyMember? retreivedMember = await _userManager.FindByNameAsync(toAdd);
        var addingTo = await _context.Neighborhood.FindAsync(fid);
        if (toAdd is null)
        {
            ModelState.AddModelError(nameof(toAdd), "please enter a valid username");
        }
        else if (retreivedMember is null)
        {
            ModelState.AddModelError(nameof(toAdd), "Could not find Family member. Is the family member registered?");
        }
        else if (addingTo is null)
        {
            ModelState.AddModelError(nameof(addingTo), "Could not find Family");
        }
        else
        {
            retreivedMember.FamilyUnitId = addingTo.FamilyUnitId;
            addingTo.Members.Add(retreivedMember);
            _context.Neighborhood.Update(addingTo);
            _context.SaveChanges();

            await _userManager.UpdateAsync(retreivedMember);
        }
        return RedirectToAction("Index", "Home");
    }
    #endregion
}

