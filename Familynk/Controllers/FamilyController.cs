using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Familynk.Controllers;

public class FamilyController : Controller
{
    private readonly SignInManager<FamilyMember> _signInManager;
    private readonly UserManager<FamilyMember> _userManager;
    private readonly FamilyContext _context;
    private readonly INotyfService _toast; 

    public FamilyMember CurrentUser {get;set;}
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
        return View();
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
    public async Task CreateNewFamily(WelcomeVM wvm)
    {
        FamilyUnit family = new() { FamilyName = wvm.NewFamily.FamilyName };
        _context.Neighborhood.Add(family);
        await _context.SaveChangesAsync();
        family = _context.Neighborhood.First(f => f.FamilyName.Equals(family.FamilyName));
        CurrentUser.Family = family;
        var result = await _userManager.UpdateAsync(CurrentUser);
        if (result.Succeeded)
        {
            _toast.Success($"Succesfully created the {family.FamilyName} Family");
            return;
        }
        _toast.Error("Unable to create new family");
    }
    #endregion
}

