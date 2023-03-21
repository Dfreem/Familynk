using System;
namespace Familynk.ViewModels
{
    public class CalendarVM
    {
        public FamilyCalendar GetCalendar { get; set; } = default!;
        public string FamilyName { get; set; } = default!;
        List<EventVm> Events { get; set; } = new();
       
    }
}

