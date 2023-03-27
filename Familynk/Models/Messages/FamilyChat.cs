using System;

namespace Familynk.Models.Messages;

public class FamilyChat
{
    public int FamilyChatId { get; set; }
    public List<FamilyMessage> Messages { get; set; } = new();
    [NotMapped]
    public FamilyMessage NewMessage { get; set; } = new();
    [ForeignKey(nameof(FamilyUnit))]
    public int FamilyUnitId { get; set; }
    public FamilyUnit Family { get; set; } = default!;
}

