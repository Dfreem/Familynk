using Familynk.Models;

namespace Familynk.ViewModels;

public class EventVM
{
    
   
    public FamilyMember Creator { get; set; } = default!;
    public DateOnly EventDate { get; set; }
    public FamilyCalendar GetCalendar { get; set; } = default!;
    public FamilyEvent Edit { get; set; } = new();

    public EventVM()
    {

    }
    public EventVM(FamilyEvent familyEvent)
    {
        Edit = familyEvent;
        Edit.Comments = familyEvent.Comments;
        EventDate = DateOnly.FromDateTime(familyEvent.EventDate);
        Edit.Title = familyEvent.Title;
        Edit.Details = familyEvent.Details;
    }
    public static explicit operator FamilyEvent(EventVM evm)
    {
        return evm.Edit;
    }
}

