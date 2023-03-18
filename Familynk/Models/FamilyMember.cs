using System;
namespace Familynk.Models;

public class FamilyMember : IdentityUser
{
    [MinLength(1, ErrorMessage = "Please enter your name")]
    [MaxLength(20, ErrorMessage = "Name cnnot be longer than 20 characters")]
    public FamilyUnit? Family { get; set; } = default!;
    [Required]
    public string FamilyName { get; set; } = default!;
    public DateTime Birthday { get; set; } = DateTime.Now;
    // I don't think this is needed
    //[NotMapped]
    //public List<string> Roles { get; set; } = new();
    public string Name { get; set; } = "";
    public List<MemberTag> TaggedIn { get; set; } = new();
    public List<MemberTag> Tags { get; set; } = new();

    public List<DirectMessage> DMsRecieved { get; set; } = new();
    public List<DirectMessage> DMsSent { get; set; } = new();
    
}

