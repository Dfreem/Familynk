using System;
namespace Familynk.ViewModels;

public class CalendarVM
{
    public FamilyCalendar GetCalendar { get; set; } = new();
    public string FamilyName { get; set; } = default!;
    public List<EventVM> Events { get; set; } = new();
    public EventVM Edit { get; set; } = new();

    public CalendarVM()
    {

    }
    public CalendarVM(FamilyCalendar familyCalendar)
    {
        GetCalendar.Events.AddRange(familyCalendar.Events);
        FamilyName = familyCalendar.FamilyName!;
    }
}

