namespace Familynk.ViewModels;

public class EventVM
{
    
    public string Title { get; set; } = "";
    public string? Details { get; set; }
    public FamilyMember Creator { get; set; } = default!;
    public DateTime EventDate { get; set; }
    public FamilyCalendar GetCalendar { get; set; } = default!;
    public List<Comment> Comments { get; set; } = new();

}

