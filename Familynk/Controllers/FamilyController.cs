using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Familynk.Controllers;

public class FamilyController : Controller
{
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
}

