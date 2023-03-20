namespace Familynk.Controllers;

public class CalendarController : Controller
{
    IServiceProvider _services;
    public CalendarController(IServiceProvider services)
    {
        _services = services;
    }

    public async Task<IActionResult> AddEvent(FamilyCalendar familyCalendar)
    {
        var context = _services.GetRequiredService<FamilyContext>();
        var famCal = await context.FamilyCalendars.FindAsync(familyCalendar.FamilyCalendarId);
        if (famCal is not null)
        {
            famCal?.Events?.Add(familyCalendar.NewEvent);
            context.FamilyCalendars.Update(famCal!);
            await context.SaveChangesAsync();
        }
        return RedirectToAction("FamilyRoom", "Family");
    }
}

