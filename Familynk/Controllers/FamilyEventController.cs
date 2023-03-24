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
        EventVM evm = new() { Creator = CurrentUser };
        return View(evm);
    }

    // POST: Event/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title, EventDate, Details, Creator")] EventVM familyEvent)
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
        var _event = await _context.Events.FirstAsync(e => e.FamilyEventId.Equals(toDelete!.FamilyEventId));
        _event.Comments.Remove(toDelete);
        _context.Comments.Remove(toDelete);
        _context.Events.Update(_event);
        return RedirectToAction(nameof(Index), "Calendar");
    }

    private bool FamilyEventExists(int id)
    {
        return (_context.Events?.Any(e => e.FamilyEventId == id)).GetValueOrDefault();
    }
}
