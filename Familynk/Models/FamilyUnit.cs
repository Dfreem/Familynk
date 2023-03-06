using Ical.Net;

namespace Familynk.Models;

public class FamilyUnit
{
    public int FamilyUnitId { get; set; }
    public string FamilyName { get; set; } = "";
    public List<FamilyMember> FamilyMembers { get; set; } = new();
    public ScrapBook FamilyScraps { get; set; } = new();
    public Calendar FamilyCalendar { get; set; } = new();
    public HouseRules Rules { get; set; } = new();
    public IMessageContainer FrontPorch { get; set; } = new GreetingsMat();

}

