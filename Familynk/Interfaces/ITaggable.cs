namespace Familynk.Interfaces;

public interface ITaggable
{
    public string? MemberTagId { get; set; }
    public string? SenderId { get; set; }

    Task ReceiveMessage(string user, string message);
}