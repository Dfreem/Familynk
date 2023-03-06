

namespace Familynk.Models;

public class ScrapBook : ITaggable
{
    public List<FamilyMember> TaggedMembers { get; set; } = new();
    List<Scrap> Entries { get; set; } = new();
    List<byte[]>? Photos { get; set; } = new();
}