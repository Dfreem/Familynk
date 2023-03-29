namespace Familynk.Models.Messages;

public class Notification : ChatMessage
{
    public int NotificationId { get; set; }
    public FamilyMember Recipient { get; set; } = default!;
    public override TimeSpan LifeSpan { get => new(3, 0, 0, 0); }
}

