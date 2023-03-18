namespace Familynk.Models;

public class FamilyMessage : AppMessage, ITaggable
{
    // AppMessage AND ITaggable both require a SenderId
    public int FamilyMessageId { get; set; }
    public override TimeSpan LifeSpan { get => new(7, 0, 0, 0); }
    public int MemberTagId { get; set; }
}

