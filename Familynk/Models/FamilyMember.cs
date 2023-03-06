using System;
namespace Familynk.Models;

public class FamilyMember
{
    public int FamilyMemberId { get; set; }
    public string Name { get; set; } = "no name";
    public DateOnly Birthday { get; set; }

    protected List<MemberTag> TaggedIn { get; set; } = new();
    protected List<DirectMessage> DMs { get; set; } = new();
    protected List<AppMessage> SentMessages { get; set; } = new();
}

