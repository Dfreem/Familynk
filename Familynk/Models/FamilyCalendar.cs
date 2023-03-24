namespace Familynk.Models;

public class FamilyCalendar
{
    public int FamilyCalendarId { get; set; }
    public int FamilyId { get; set; }
    public string? FamilyName { get; set; } = default!;
    public List<FamilyEvent> Events { get; set; } = new();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [NotMapped]
    public FamilyEvent NewEvent { get; set; } = new();
}

