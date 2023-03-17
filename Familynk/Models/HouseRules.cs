namespace Familynk.Models;

public class HouseRules
{
    public int HouseRulesId { get; set; }
    public TimeSpan MagneticMessageLifespan { get; set; }
    public TimeSpan StickyNoteLifespan { get; set; }
    public bool FamilyMembersCustomizeKitchen { get; set; }
    public bool FamilyMembersCreateEvents { get; set; }
    public bool FamilyMembersInviteOtherMembers { get; set; }
}