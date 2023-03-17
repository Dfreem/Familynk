namespace Familynk.Models;

public abstract class AppMessage
{
    public abstract string Body { get; set; }
    public abstract FamilyMember Sender { get; set; }
    /// <summary>
    /// How long does the message last before it is released from memory.
    /// A value of <see cref="TimeSpan.Zero"/> indicates no  limit to the message lifetime.
    /// </summary>
    public abstract TimeSpan LifeSpan { get; init; }
}