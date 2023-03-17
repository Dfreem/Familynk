using System;
namespace Familynk.Models;

public class Comment : AppMessage
{
    public int CommentId { get; set; }
    public override string Body { get; set; } = "";
    public bool IsReply { get; set; }
    public override FamilyMember Sender { get; set; } = default!;
    public override TimeSpan LifeSpan { get; init; } = TimeSpan.Zero;
}

