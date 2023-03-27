namespace Familynk.Models.Messages;

public abstract class AppMessage
{
    public virtual int AppMessageId { get; set; }
    public string? Body { get; set; }
    public virtual string? SenderId { get; set; } = default!;
    /// <summary>
    /// How long does the message last before it is released from memory.
    /// A value of <see cref="TimeSpan.Zero"/> indicates no  limit to the message lifetime.
    /// </summary>
    public abstract TimeSpan LifeSpan { get; }
}