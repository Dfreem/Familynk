using System;
namespace Familynk.Models;

public class FamilyMember : IdentityUser
{
    [MinLength(1, ErrorMessage = "Please enter your name")]
    [MaxLength(20, ErrorMessage = "Name cannot be longer than 20 characters")]
    public int? FamilyUnitId { get; set; } = default!;
    public FamilyCalendar GetFamilyCalendar { get; set; } = new();
    public DateTime? Birthday { get; set; } = null;
    // I don't think this is needed
    //[NotMapped]
    //public List<string> Roles { get; set; } = new();
    public string Name { get; set; } = "";
    public List<MemberTag> TaggedIn { get; set; } = new();
    public List<MemberTag> Tags { get; set; } = new();

    public List<DirectMessage> DMsRecieved { get; set; } = new();
    public List<DirectMessage> DMsSent { get; set; } = new();
    
}

