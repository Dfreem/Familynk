using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Familynk.Data;
using Familynk.Models;

namespace Familynk.Controllers
{
    public class CalendarController : Controller
    {
        private readonly FamilyContext _context;
        private readonly IServiceProvider _services;
        private readonly SignInManager<FamilyMember> _signInManager;
        private readonly UserManager<FamilyMember> _userManager;
        public FamilyMember CurrentUser { get; set; }



        public CalendarController(FamilyContext context, IServiceProvider services)
        {
            _context = context;
            _services = services;
            _signInManager = services.GetRequiredService<SignInManager<FamilyMember>>();
            _userManager = _signInManager.UserManager;
            CurrentUser = _userManager.FindByNameAsync(_signInManager.Context.User!.Identity!.Name!)
            .Result!;
            CurrentUser.DMsSent = _context.DMs.Where(m => m.SenderId!.Equals(CurrentUser.Id)).ToList();
            CurrentUser.DMsRecieved = _context.DMs.Where(m => m.RecipientId.Equals(CurrentUser.Id)).ToList();
        }

        // GET: Calander
        public async Task<IActionResult> Index()
        {
            var family = await _context.Neighborhood.FindAsync(CurrentUser.FamilyUnitId);
            var calendar = family?.GetCalendar;
            calendar!.Events = _context.Events.Where(e => e.CalendarId.Equals(family!.GetCalendar.FamilyCalendarId)).ToList();
            CalendarVM cvm = new()
            {
                FamilyName = family!.FamilyName,
                GetCalendar = calendar
            };
            return View(cvm);
        }

        // GET: Calander/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FamilyCalendars == null)
            {
                return NotFound();
            }

            var familyCalendar = await _context.FamilyCalendars
                .FirstOrDefaultAsync(m => m.FamilyCalendarId == id);
            if (familyCalendar == null)
            {
                return NotFound();
            }

            return View(familyCalendar);
        }

       
    }
}
