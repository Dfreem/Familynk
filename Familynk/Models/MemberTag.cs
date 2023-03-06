namespace Familynk.Models;

public class MemberTag : ITaggable
{
    int MemberTagId { get; set; }
    FamilyMember Sender { get; init; } = default!;
    public List<FamilyMember> TaggedMembers { get; set; } = new();
}