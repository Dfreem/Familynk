namespace Familynk.Models;

public class MemberTag : ITaggable
{
    public int MemberTagId { get; set; }
    public FamilyMember Sender { get; init; } = default!;
    public List<FamilyMember> TaggedMembers { get; set; } = new();
}