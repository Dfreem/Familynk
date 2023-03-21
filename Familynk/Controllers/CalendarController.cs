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
            CurrentUser.DMsSent = _context.DMs.Where(m => m.SenderId.Equals(CurrentUser.Id)).ToList();
            CurrentUser.DMsRecieved = _context.DMs.Where(m => m.RecipientId.Equals(CurrentUser.Id)).ToList();
        }

        // GET: Calander
        public async Task<IActionResult> Index()
        {
            var family = await _context.Neighborhood.FindAsync(CurrentUser.FamilyUnitId);
            var calendar = family?.GetCalendar;
            return View(calendar);
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

        // GET: Calander/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calander/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FamilyCalendarId,FamilyId,FamilyName,StartDate,EndDate")] FamilyCalendar familyCalendar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(familyCalendar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(familyCalendar);
        }

        // GET: Calander/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FamilyCalendars == null)
            {
                return NotFound();
            }

            var familyCalendar = await _context.FamilyCalendars.FindAsync(id);
            if (familyCalendar == null)
            {
                return NotFound();
            }
            return View(familyCalendar);
        }

        // POST: Calander/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FamilyCalendarId,FamilyId,FamilyName,StartDate,EndDate")] FamilyCalendar familyCalendar)
        {
            if (id != familyCalendar.FamilyCalendarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(familyCalendar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyCalendarExists(familyCalendar.FamilyCalendarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(familyCalendar);
        }

        // GET: Calander/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Calander/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FamilyCalendars == null)
            {
                return Problem("Entity set 'FamilyContext.FamilyCalendars'  is null.");
            }
            var familyCalendar = await _context.FamilyCalendars.FindAsync(id);
            if (familyCalendar != null)
            {
                _context.FamilyCalendars.Remove(familyCalendar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyCalendarExists(int id)
        {
          return (_context.FamilyCalendars?.Any(e => e.FamilyCalendarId == id)).GetValueOrDefault();
        }
    }
}
