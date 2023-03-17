namespace Familynk.Models;

public class Scrap : AppMessage, ITaggable
{
    public int ScrapId { get; set; }
    TimeSpan timeLimit = TimeSpan.Zero;
    public List<FamilyMember> TaggedMembers { get; set; } = new();
    public List<Image>? Images { get; set; }
    public string Title { get; set; } = "";
    public override string Body { get; set; } = "";
    public List<Comment> Comments { get; set; } = new();
    public override FamilyMember Sender { get; set; } = default!;

    public override TimeSpan LifeSpan { get; init; } = TimeSpan.Zero;
}