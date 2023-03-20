namespace Familynk.Controllers;

public class DirectMessageController : Controller
{
    private readonly FamilyContext _context;
    private readonly IServiceProvider _services;
    private readonly SignInManager<FamilyMember> _signInManager;
    private readonly UserManager<FamilyMember> _userManager;
    public FamilyMember CurrentUser { get; set; }



    public DirectMessageController(FamilyContext context, IServiceProvider services)
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

    // GET: DirectMessageControlller
    public IActionResult Index()
    {

        DMVM dms = new();
        foreach (var message in CurrentUser.DMsRecieved)
        {
            dms.RecievedMessages.Add(message);
        }
        foreach (var dm in CurrentUser.DMsSent)
        {
            dms.SentMessages.Add(dm);
        }
        dms.Contacts.AddRange(_userManager.Users.Where((FamilyMember arg) => arg.FamilyUnitId.Equals(CurrentUser.FamilyUnitId)));
        return View(dms);
    }

    // GET: DirectMessageControlller/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.DMs == null)
        {
            return NotFound();
        }

        var directMessage = await _context.DMs
            .FirstOrDefaultAsync(m => m.AppMessageId == id);
        if (directMessage == null)
        {
            return NotFound();
        }

        return View(directMessage);
    }

    // GET: DirectMessageControlller/Create
    public IActionResult Create()
    {
        DMVM dms = new()
        {
            Owner = CurrentUser,
            Contacts = _userManager.Users
                .Where(f => f.FamilyUnitId
                .Equals(CurrentUser.FamilyUnitId))
                .ToList()
        };
        return View(dms);
    }

    // POST: DirectMessageControlller/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("NewMessage, SendMessageTo, Owner")] DMVM dmvm)
    {
        var directMessage = dmvm.NewMessage;
        directMessage!.SenderId = CurrentUser.Id;
        directMessage!.RecipientId = dmvm.SendMessageTo!;

        if (ModelState.GetFieldValidationState(nameof(dmvm.SendMessageTo)) == ModelValidationState.Valid)
        {
            var recipient = await _userManager.FindByIdAsync(directMessage.RecipientId);
            recipient!.DMsRecieved.Add(directMessage);
            await _userManager.UpdateAsync(recipient);

            CurrentUser.DMsSent.Add(directMessage);
            await _userManager.UpdateAsync(CurrentUser);

            //_context.DMs.Add(directMessage);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    // GET: DirectMessageControlller/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.DMs == null)
        {
            return NotFound();
        }

        var directMessage = await _context.DMs.FindAsync(id);
        if (directMessage == null)
        {
            return NotFound();
        }
        return View(directMessage);
    }

    // POST: DirectMessageControlller/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("AppMessageId,RecipientId,AppMessageId,Body,FamilyEventId")] DirectMessage directMessage)
    {
        if (id != directMessage.AppMessageId)
        {
            return NotFound();
        }

        if (ModelState.GetFieldValidationState("Body") == ModelValidationState.Valid)
        {
            var tempMessage = _context.DMs.Find(directMessage.AppMessageId);
            try
            {
                tempMessage!.Body = directMessage.Body;
                _context.DMs.Update(tempMessage!);
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectMessageExists(directMessage.AppMessageId))
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
        return View(directMessage);
    }

    // GET: DirectMessageControlller/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.DMs == null)
        {
            return NotFound();
        }

        var directMessage = await _context.DMs
            .FirstOrDefaultAsync(m => m.AppMessageId == id);
        if (directMessage == null)
        {
            return NotFound();
        }

        return View(directMessage);
    }

    // POST: DirectMessageControlller/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.DMs == null)
        {
            return Problem("Entity set 'FamilyContext.DMs'  is null.");
        }
        var directMessage = await _context.DMs.FindAsync(id);
        if (directMessage != null)
        {
            _context.DMs.Remove(directMessage);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DirectMessageExists(int id)
    {
        return (_context.DMs?.Any(e => e.AppMessageId == id)).GetValueOrDefault();
    }
}
