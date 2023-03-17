namespace Familynk.Models;

public class DirectMessage : AppMessage
{
    public int DirectMessageId { get; set; }
    public override string Body { get; set; } = "";
    public override FamilyMember Sender { get; set; } = default!;
    public FamilyMember Recipient { get; set; } = default!;
    public override TimeSpan LifeSpan { get; init; } = TimeSpan.Zero;
}