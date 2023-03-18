
namespace Familynk.Models;

public class FamilyEvent : ITaggable
{
    public int FamilyEventId { get; set; }
    public int MemberTagId { get; set; }
    public int SenderId { get; set; }
}

