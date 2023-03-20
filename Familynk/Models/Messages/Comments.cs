﻿namespace Familynk.Models.Messages;

public class Comment : AppMessage
{
    public int CommentId { get; set; }
    public bool IsReply { get; set; }
    public override TimeSpan LifeSpan { get => TimeSpan.Zero; }
}
