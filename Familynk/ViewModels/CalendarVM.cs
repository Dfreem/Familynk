using System;
namespace Familynk.ViewModels
{
    public class CalendarVM
    {
        public FamilyCalendar GetCalendar { get; set; } = default!;
        public string FamilyName { get; set; } = default!;
        List<EventVM> Events { get; set; } = new();
       
    }
}

