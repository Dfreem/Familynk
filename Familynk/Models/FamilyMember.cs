using System;
namespace Familynk.Models;

public class FamilyMember : IdentityUser
{
    public string Name { get; set; } = "no name";
    public string FamilyName { get; set; } = "";
    public DateOnly Birthday { get; set; }
    // I don't think this is needed
    //[NotMapped]
    //public List<string> Roles { get; set; } = new();

    protected List<MemberTag> TaggedIn { get; set; } = new();
    protected List<DirectMessage> DMs { get; set; } = new();
    protected List<AppMessage> SentMessages { get; set; } = new();
}

