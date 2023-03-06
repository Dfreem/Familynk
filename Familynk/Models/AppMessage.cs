namespace Familynk.Models;

public abstract class AppMessage
{
    public abstract string Body { get; set; }
    public abstract FamilyMember Sender { get; set; }
    public abstract TimeSpan LifeSpan { get; init; }
}