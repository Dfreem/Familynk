namespace Familynk.Models;

public class Scrap : ITaggable
{
    public int ScrapId { get; set; }
    public int ScrapBookId { get; set; }
    TimeSpan timeLimit = TimeSpan.Zero;
    public List<Image>? Images { get; set; }
    public string Title { get; set; } = "";
    public List<Comment>? Comments { get; set; } = new();
    public string? MemberTagId { get; set; }
    public string? SenderId { get; set; }
}