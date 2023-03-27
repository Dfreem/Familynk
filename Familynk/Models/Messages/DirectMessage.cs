namespace Familynk.Models.Messages;

public class DirectMessage : AppMessage
{
    public int DirectMessageId { get; set; }
    public string RecipientId { get; set; } = default!;
    public override TimeSpan LifeSpan { get => TimeSpan.Zero; }
}