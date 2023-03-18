namespace Familynk.Models;

public abstract class AppMessage
{
    public int AppMessageId { get; set; }
    public string? Body { get; set; }
    public int SenderId { get; set; }
    /// <summary>
    /// How long does the message last before it is released from memory.
    /// A value of <see cref="TimeSpan.Zero"/> indicates no  limit to the message lifetime.
    /// </summary>
    public abstract TimeSpan LifeSpan { get; }
}