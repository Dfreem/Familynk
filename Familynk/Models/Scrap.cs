namespace Familynk.Models;

public class Scrap : ITaggable
{
    public int ScrapId { get; set; }
    TimeSpan timeLimit = TimeSpan.Zero;
    public List<Image>? Images { get; set; }
    public string Title { get; set; } = "";
    public List<Comment> Comments { get; set; } = new();
    public int MemberTagId { get; set; }
    public int SenderId { get; set; }
}