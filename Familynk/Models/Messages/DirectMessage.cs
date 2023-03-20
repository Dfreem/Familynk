namespace Familynk.Models.Messages;

[PrimaryKey(nameof(AppMessageId))]
public class DirectMessage : AppMessage
{
    public string RecipientId { get; set; } = default!;
    public override TimeSpan LifeSpan { get => TimeSpan.Zero; }
}