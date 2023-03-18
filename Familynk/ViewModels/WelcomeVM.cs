using System;

namespace Familynk.ViewModels
{
    public class WelcomeVM
    {
        public FamilyUnit NewFamily { get; set; } = new();
        public List<FamilyUnit> Neighborhood { get; set; } = new();
        [Required]
        public FamilyMember? Visitor { get; set; } = default!;
    }
}

