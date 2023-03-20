using System;
using System.Collections.Immutable;

namespace Familynk.Util
{
    public static class CalendarHelpers
    {
        static int SinceSunday => DateTime.Now.DayOfWeek - DayOfWeek.Sunday;
        static DateTime start => DateTime.Now.AddDays(-SinceSunday);

        public static DateTime[] GetDaySpan(int numDays)
        {
            DateTime[] thisWeek = new DateTime[numDays];
            for (int i = 0; i < numDays; i++)
            {
                thisWeek[i] = start.AddDays(i);
            }
            return thisWeek;
        }

        public static FamilyEvent ParseEvent(this FamilyEvent fv,CalendarEvent calendarEvent)
        {
            return new()
            {
                EventDate = calendarEvent.DtStart.AsSystemLocal,
                Details = calendarEvent.Description,
                Title = calendarEvent.Name
            };
        }
    }
}

