
namespace Familynk.Models;

public class FamilyEvent : ITaggable
{
    public int FamilyEventId { get; set; }
    public List<FamilyMember> TaggedMembers { get; set; } = new();
}

