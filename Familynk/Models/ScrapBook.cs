

namespace Familynk.Models;

public class ScrapBook : ITaggable
{
    public int ScrapBookId { get; set; }
    public List<Scrap>? Entries { get; set; } = new();
    public List<Image>? Photos { get; set; } = new();
    public string? MemberTagId { get; set; }
    public string? SenderId { get; set; }
}