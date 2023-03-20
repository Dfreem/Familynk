using Ical.Net;

namespace Familynk.Models;

public class FamilyUnit
{
    public int FamilyUnitId { get; set; }
    [Display(Name = "Family Name")]
    public string FamilyName { get; set; } = "";
    public List<FamilyMember> Members { get; set; } = new();
    public ScrapBook FamilyScraps { get; set; } = new();
    public HouseRules Rules { get; set; } = new();
    public FamilyCalendar GetCalendar { get; set; } = new();
    public List<FamilyMessage> FamilyChat { get; set; } = new();
}

