namespace Familynk.Controllers;

public class TagController : Controller
{
    private readonly FamilyContext _context;

    public TagController(FamilyContext context)
    {
        _context = context;
    }

    // GET: Tag
    public async Task<IActionResult> Index()
    {
          return _context.Tags != null ? 
                      View(await _context.Tags.ToListAsync()) :
                      Problem("Entity set 'FamilyContext.Tags'  is null.");
    }

    // GET: Tag/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null || _context.Tags == null)
        {
            return NotFound();
        }

        var memberTag = await _context.Tags
            .FirstOrDefaultAsync(m => m.MemberTagId == id);
        if (memberTag == null)
        {
            return NotFound();
        }

        return View(memberTag);
    }

    // GET: Tag/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Tag/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FamilyEventId,SenderId,MemberTagId")] MemberTag memberTag)
    {
        if (ModelState.IsValid)
        {
            _context.Add(memberTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(memberTag);
    }

    // GET: Tag/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null || _context.Tags == null)
        {
            return NotFound();
        }

        var memberTag = await _context.Tags.FindAsync(id);
        if (memberTag == null)
        {
            return NotFound();
        }
        return View(memberTag);
    }

    // POST: Tag/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("FamilyEventId,SenderId,MemberTagId")] MemberTag memberTag)
    {
        if (id != memberTag.MemberTagId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(memberTag);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberTagExists(memberTag.MemberTagId))
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
        return View(memberTag);
    }

    // GET: Tag/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null || _context.Tags == null)
        {
            return NotFound();
        }

        var memberTag = await _context.Tags
            .FirstOrDefaultAsync(m => m.MemberTagId == id);
        if (memberTag == null)
        {
            return NotFound();
        }

        return View(memberTag);
    }

    // POST: Tag/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (_context.Tags == null)
        {
            return Problem("Entity set 'FamilyContext.Tags'  is null.");
        }
        var memberTag = await _context.Tags.FindAsync(id);
        if (memberTag != null)
        {
            _context.Tags.Remove(memberTag);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MemberTagExists(string id)
    {
      return (_context.Tags?.Any(e => e.MemberTagId == id)).GetValueOrDefault();
    }
}
