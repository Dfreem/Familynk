using System;
using AspNetCore;
using NuGet.Protocol.Plugins;

namespace Familynk.ViewModels;

public class EventVm
{
    
    public string Title { get; set; } = "";
    public string? Details { get; set; }
    private FamilyMember _sender = new();
    public FamilyMember Creator { get; set; } = default!;
    public DateTime EventDate { get; set; }
    public FamilyCalendar GetCalendar { get; set; } = default!;

   
}

