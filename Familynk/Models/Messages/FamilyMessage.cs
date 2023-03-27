namespace Familynk.Models.Messages;

public class FamilyMessage : AppMessage
{
    public int FamilyMessageId { get; set; }
    public override TimeSpan LifeSpan { get => new(7, 0, 0, 0); }

    [ForeignKey(nameof(Sender))]
    public string FamilyMemberId { get; set; } = default!;
    public FamilyMember Sender { get; set; } = default!;

    [ForeignKey(nameof(Family))]
    public int FamilyUnitId { get; set; }
    [Required]
    public FamilyUnit Family { get; set; } = default!;
}

