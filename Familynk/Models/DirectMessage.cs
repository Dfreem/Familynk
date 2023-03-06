namespace Familynk.Models
{
    public class DirectMessage : AppMessage
    {
        public override string Body { get; set; } = "";
        public override FamilyMember Sender { get; set; } = default!;
        public override TimeSpan LifeSpan { get; set; } = default!;
    }
}