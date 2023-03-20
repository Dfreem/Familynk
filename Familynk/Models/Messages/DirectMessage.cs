namespace Familynk.Models.Messages;

public class DirectMessage : AppMessage
{
    public int DirectMessageId { get; set; }
    public int RecipientId { get; set; }
    public override TimeSpan LifeSpan { get => TimeSpan.Zero; }
}