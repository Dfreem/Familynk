namespace Familynk.Controllers;

public class FamilyEventController : Controller
{
    private readonly FamilyContext _context;
    private readonly IServiceProvider _services;
    private readonly SignInManager<FamilyMember> _signInManager;
    private readonly UserManager<FamilyMember> _userManager;
    public FamilyMember CurrentUser { get; set; }



    public FamilyEventController(FamilyContext context, IServiceProvider services)
    {
        _context = context;
        _services = services;
        _signInManager = services.GetRequiredService<SignInManager<FamilyMember>>();
        _userManager = _signInManager.UserManager;
        CurrentUser = _userManager.FindByNameAsync(_signInManager.Context.User!.Identity!.Name!)
        .Result!;
    }
    // GET: Event
    public async Task<IActionResult> Index()
    {
        var fam = await _context.Neighborhood.FindAsync(CurrentUser.FamilyUnitId);
        return _context.Events != null ?
                    View(await _context.Events.Where(e => e.CalendarId.Equals(fam!.GetCalendar.FamilyCalendarId)).Include(c => c.Comments).ToListAsync()) :
                    Problem("Entity set 'FamilyContext.Events'  is null.");
    }

    // GET: Event/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Events == null)
        {
            return NotFound();
        }

        var familyEvent = await _context.Events
            .FirstOrDefaultAsync(m => m.FamilyEventId == id);
        if (familyEvent == null)
        {
            return NotFound();
        }

        return View(familyEvent);
    }

    // GET: Event/Create
    public IActionResult Create()
    {
        EventVm evm = new() { Creator = CurrentUser };
        return View(evm);
    }

    // POST: Event/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title, EventDate, Details, Creator")] EventVm familyEvent)
    {
        if (ModelState.GetFieldValidationState("Title") == ModelValidationState.Valid)
        {
            FamilyEvent famEvent = new()
            {

                Details = familyEvent.Details,
                EventDate = familyEvent.EventDate,
                SenderId = CurrentUser.Id,
                Title = familyEvent.Title

            };
            _context.Events.Add(famEvent);
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
        return View(familyEvent);
    }

    // POST: Event/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("FamilyEventId,MemberTagId,SenderId,CalendarId,EventDate,Title,Details")] FamilyEvent familyEvent)
    {
        if (id != familyEvent.FamilyEventId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(familyEvent);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyEventExists(familyEvent.FamilyEventId))
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
        return View(familyEvent);
    }

    // GET: Event/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Events == null)
        {
            return NotFound();
        }

        var familyEvent = await _context.Events
            .FirstOrDefaultAsync(m => m.FamilyEventId == id);
        if (familyEvent == null)
        {
            return NotFound();
        }

        return View(familyEvent);
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
        return RedirectToAction(nameof(Index));
    }

    private bool FamilyEventExists(int id)
    {
        return (_context.Events?.Any(e => e.FamilyEventId == id)).GetValueOrDefault();
    }
}
