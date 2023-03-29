using System;
namespace Familynk.Models;

public class FamilyMember : IdentityUser
{
    public int? FamilyUnitId { get; set; } = default!;
    public DateTime? Birthday { get; set; } = null;
    [NotMapped]
    public List<string> Roles { get; set; } = new();
    [MinLength(1, ErrorMessage = "Please enter your name")]
    [MaxLength(20, ErrorMessage = "Name cannot be longer than 20 characters")]
    public string Name { get; set; } = "";
    public int MessageBubbleColor { get; set; }

}

