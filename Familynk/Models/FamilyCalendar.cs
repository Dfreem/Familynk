namespace Familynk.Models;

public class FamilyCalendar
{
    public int FamilyCalendarId { get; set; }
    public string SerializedCalendar { get; set; } = default!;
    public int FamilyId { get; set; }
}

