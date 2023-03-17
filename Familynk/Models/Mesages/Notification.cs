namespace Familynk.Models;

public class Notification : AppMessage
{
    public int NotificationId { get; set; }
    public override string Body { get; set; } = "";
    public override FamilyMember Sender { get; set; } = default!;
    public FamilyMember Recipient { get; set; } = default!;
    public override TimeSpan LifeSpan { get; init; } = new(3, 0, 0, 0);
}

