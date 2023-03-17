namespace Familynk.Models;

public class FamilyMessage : AppMessage, ITaggable
{
    public int FamilyMessageId { get; set; }
    public override string Body { get; set; } = "";
    public override FamilyMember Sender { get; set; } = default!;
    public override TimeSpan LifeSpan { get; init; } = new(7, 0, 0, 0);
    public List<FamilyMember> TaggedMembers { get; set; } = new();
}

