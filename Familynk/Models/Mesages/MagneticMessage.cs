namespace Familynk.Models;

public class MagneticMessage : AppMessage
{
    public int MagneticMessageId { get; set; }
    public override string Body { get; set; } = "";
    public override FamilyMember Sender { get; set; } = default!;
    public override TimeSpan LifeSpan { get; init; } = new(1, 0, 0);
    public Image? Picture { get; set; }
}

