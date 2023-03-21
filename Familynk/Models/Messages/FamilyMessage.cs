namespace Familynk.Models.Messages;

public class FamilyMessage : AppMessage, ITaggable
{
    // AppMessage AND ITaggable both require a SenderId
    public int FamilyMessageId { get; set; }
    public override TimeSpan LifeSpan { get => new(7, 0, 0, 0); }
    public string? MemberTagId { get; set; }
    [ForeignKey(nameof(FamilyMember))]
    public string SenderName { get; set; } = "";
    [ForeignKey(nameof(FamilyUnit))]
    public int? FamilyUnitId { get; set; }
    [Required]
    public FamilyUnit Family { get; set; } = default!;
}

