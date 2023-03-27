namespace Familynk.Models.Messages;

public class Comment : AppMessage
{
    public int CommentId { get; set; }
    public bool IsReply { get; set; }
    public bool HasReply { get; set; }
    [ForeignKey(nameof(OriginalPost))]
    public int? ReplyTo { get; set; }
    public AppMessage? OriginalPost { get; set; }
    public override TimeSpan LifeSpan { get => TimeSpan.Zero; }
}

