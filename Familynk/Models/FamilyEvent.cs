
namespace Familynk.Models;

public class FamilyEvent
{
    public int FamilyEventId { get; set; }
    public string? MemberTagId { get; set; }
    public string? SenderId { get; set; }
    public int CalendarId { get; set; }
    public DateTime EventDate { get; set; }
    [MaxLength(20, ErrorMessage = "Title can't be longer than 20 characters")]
    [Required(ErrorMessage = "Please enter event title")]
    public string Title { get; set; } = default!;
    [MaxLength(300, ErrorMessage = "please limit details to less than 300 characters")]
    public string? Details { get; set; } = default;
    public List<Comment> Comments { get; set; } = new();

}

