﻿namespace Familynk.Models;

public class MemberTag : ITaggable
{
    public int FamilyEventId { get; set; }
    public string? SenderId { get; set; }
    public string? MemberTagId { get; set; }

    public Task ReceiveMessage(string user, string message)
    {
        throw new NotImplementedException();
    }
}