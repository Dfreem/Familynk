

namespace Familynk.Models;

public class ScrapBook : ITaggable
{
    public int ScrapBookId { get; set; }
    public List<Scrap> Entries { get; set; } = new();
    public List<Image>? Photos { get; set; } = new();
    public int MemberTagId { get; set; }
    public int SenderId { get; set; }
}