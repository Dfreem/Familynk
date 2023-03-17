

namespace Familynk.Models;

public class ScrapBook : ITaggable
{
    public int ScrapBookId { get; set; }
    public List<FamilyMember> TaggedMembers { get; set; } = new();
    public List<Scrap> Entries { get; set; } = new();
    public List<Image>? Photos { get; set; } = new();
}