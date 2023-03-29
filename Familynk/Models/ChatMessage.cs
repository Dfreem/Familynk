namespace Familynk.Models.Messages;

public class ChatMessage
{

    public int AppMessageId { get; set; }
    public string Message { get; set; } = default!;
    [ForeignKey(nameof(Sender))]
    public virtual string SenderId { get; set; } = default!;
    public virtual FamilyMember Sender { get; set; } = default!;
}