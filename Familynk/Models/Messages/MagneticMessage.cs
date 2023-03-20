namespace Familynk.Models.Messages;

public class MagneticMessage : AppMessage
{
    public int MagneticMessageId { get; set; }
    public override TimeSpan LifeSpan { get => new(1, 0, 0); }
    public Image? Picture { get; set; }
}

