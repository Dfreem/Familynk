namespace Familynk.Models;

public class Scrap : AppMessage, ITaggable
{
    TimeSpan timeLimit = TimeSpan.Zero;

    public List<FamilyMember> TaggedMembers { get; set; } = new();
    public List<byte[]>? Images { get; set; }
    public string Title { get; set; } = "";
    public override string Body { get; set; } = "";
    public List<string> Comments { get; set; } = new();
    public override FamilyMember Sender { get; set; } = default!;
    public override TimeSpan LifeSpan
    {
        get
        {
            return timeLimit;
        }
        init
        {
            timeLimit = value;
        }
    }
}