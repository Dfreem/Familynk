using Microsoft.IdentityModel.Tokens;

namespace Familynk.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly FamilyContext _context;
        private readonly IServiceProvider _services;
        private readonly SignInManager<FamilyMember> _signInManager;
        private readonly UserManager<FamilyMember> _userManager;
        public FamilyMember CurrentUser { get; set; }
        public FamilyUnit? CurrentFamily { get; set; }


        public CalendarController(FamilyContext context, IServiceProvider services)
        {
            _context = context;
            _services = services;
            _signInManager = services.GetRequiredService<SignInManager<FamilyMember>>();
            _userManager = _signInManager.UserManager;
            string? uName = _signInManager.Context.User.Identity?.Name;
            CurrentUser = _userManager
                .FindByNameAsync(uName!).Result ?? new();

            CurrentUser.DMsSent = _context.DMs.Where(m => m.SenderId!.Equals(CurrentUser.Id)).ToList();
            CurrentUser.DMsRecieved = _context.DMs.Where(m => m.RecipientId.Equals(CurrentUser.Id)).ToList();
            if (CurrentUser.FamilyUnitId is not null)
            {
                CurrentFamily = _context.Neighborhood
                    .Include(f => f.GetCalendar)
                    .ThenInclude(c => c.Events)
                    .First(f => f.FamilyUnitId
                    .Equals(CurrentUser.FamilyUnitId));
            }
            if (CurrentFamily is not null)
            {
                CurrentFamily.GetCalendar = _context.FamilyCalendars.Find(CurrentFamily?.FamilyUnitId) ?? new();
            }
        }

        // GET: Calander
        public async Task<IActionResult> Index()
        {
            FamilyCalendar cal = await _context.FamilyCalendars.FindAsync(CurrentFamily!.GetCalendar.FamilyCalendarId) ?? new();
            var events = _context.Events
                .Where(e => e.CalendarId
                    .Equals(cal.FamilyCalendarId))
                .ToList();
            var cvm = new CalendarVM(cal ?? new());
            return View(cvm);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var familyEvent = await _context.Events
                .Include(e => e.Comments)
                .FirstOrDefaultAsync(m => m.FamilyEventId == id);
            if (familyEvent == null)
            {
                return NotFound();
            }
            EventVM evm = new(familyEvent)
            {
                Creator = CurrentUser
            };
            var savedEvents = _context.Events.Where(e => e.CalendarId.Equals(CurrentFamily!.GetCalendar.FamilyCalendarId))?.ToList() ?? new List<FamilyEvent>();
            if (!savedEvents.IsNullOrEmpty())
            {
                evm.GetCalendar.Events ??= savedEvents;
            }
            return View(evm);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            EventVM evm = new() { Creator = CurrentUser };
            return View(evm);
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( EventVM familyEvent)
        {
            if (ModelState.GetFieldValidationState("Edit.Title") == ModelValidationState.Valid &&
                ModelState.GetFieldValidationState("Edit.Details") == ModelValidationState.Valid)
            {
                FamilyEvent famEvent = (FamilyEvent)familyEvent;
                CurrentFamily?.GetCalendar.Events.Add(famEvent);
                _context.Neighborhood.Update(CurrentFamily!);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Calendar");
            }
            return View(familyEvent);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var familyEvent = await _context.Events.FindAsync(id);
            if (familyEvent == null)
            {
                return NotFound();
            }
            var cal = await _context.FamilyCalendars.FirstOrDefaultAsync(c => c.FamilyId.Equals(CurrentUser.FamilyUnitId));
            EventVM cvm = new(familyEvent)
            {
                Creator = CurrentUser,
                Edit = familyEvent
            };
            return View(cvm);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventVM familyEvent)
        {
            if (id != familyEvent.Edit.FamilyEventId)
            {
                return NotFound();
            }

            if (ModelState.GetFieldValidationState("Edit") == ModelValidationState.Valid)
            {
                try
                {
                    _context.Events.Remove(_context.Events.Find(id)!);
                    _context.Events.Update(familyEvent.Edit);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyEventExists(familyEvent.Edit.FamilyEventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "Calendar");
            }
            return View(familyEvent);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var toDelete = await _context.Events.FindAsync(id);
            if (toDelete is null)
            {
                ModelState.AddModelError(nameof(id), "could not find the event.");
                return RedirectToAction("Index", "Calendar");
            }
            _context.Events.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index), "Calendar");
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'FamilyContext.Events'  is null.");
            }
            var familyEvent = await _context.Events.FindAsync(id);
            if (familyEvent != null)
            {
                _context.Events.Remove(familyEvent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Calendar");
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            var toDelete = await _context.Comments.FindAsync(id);
            if (toDelete is null)
            {
                ModelState.AddModelError(nameof(Comment), "Could not find Comment");
                return RedirectToAction(nameof(Index));
            }
            _context.Comments.Remove(toDelete);
            return RedirectToAction(nameof(Index), "Calendar");
        }

        private bool FamilyEventExists(int id)
        {
            return (_context.Events?.Any(e => e.FamilyEventId == id)).GetValueOrDefault();
        }

    }
}
