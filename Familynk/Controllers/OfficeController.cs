namespace Familynk.Controllers
{
    [Authorize(Roles = "HOH")]
    public class OfficeController : Controller
    {
        private readonly FamilyContext _context;

        public OfficeController(FamilyContext context)
        {
            _context = context;
        }

        // GET: Office
        public async Task<IActionResult> Index()
        {
              return _context.Rules != null ? 
                          View(await _context.Rules.ToListAsync()) :
                          Problem("Entity set 'FamilyContext.Rules'  is null.");
        }

        // GET: Office/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rules == null)
            {
                return NotFound();
            }

            var houseRules = await _context.Rules
                .FirstOrDefaultAsync(m => m.HouseRulesId == id);
            if (houseRules == null)
            {
                return NotFound();
            }

            return View(houseRules);
        }

        // GET: Office/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Office/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HouseRulesId,MagneticMessageLifespan,StickyNoteLifespan,FamilyMembersCustomizeKitchen,FamilyMembersCreateEvents,FamilyMembersInviteOtherMembers")] HouseRules houseRules)
        {
            if (ModelState.IsValid)
            {
                _context.Add(houseRules);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(houseRules);
        }

        // GET: Office/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rules == null)
            {
                return NotFound();
            }

            var houseRules = await _context.Rules.FindAsync(id);
            if (houseRules == null)
            {
                return NotFound();
            }
            return View(houseRules);
        }

        // POST: Office/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HouseRulesId,MagneticMessageLifespan,StickyNoteLifespan,FamilyMembersCustomizeKitchen,FamilyMembersCreateEvents,FamilyMembersInviteOtherMembers")] HouseRules houseRules)
        {
            if (id != houseRules.HouseRulesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(houseRules);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseRulesExists(houseRules.HouseRulesId))
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
            return View(houseRules);
        }

        // GET: Office/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rules == null)
            {
                return NotFound();
            }

            var houseRules = await _context.Rules
                .FirstOrDefaultAsync(m => m.HouseRulesId == id);
            if (houseRules == null)
            {
                return NotFound();
            }

            return View(houseRules);
        }

        // POST: Office/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rules == null)
            {
                return Problem("Entity set 'FamilyContext.Rules'  is null.");
            }
            var houseRules = await _context.Rules.FindAsync(id);
            if (houseRules != null)
            {
                _context.Rules.Remove(houseRules);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HouseRulesExists(int id)
        {
          return (_context.Rules?.Any(e => e.HouseRulesId == id)).GetValueOrDefault();
        }
    }
}
