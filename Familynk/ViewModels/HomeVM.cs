namespace Familynk.ViewModels;

public class HomeVM
{
    public int FamilyUnitId { get; set; }
    public string FamilyName { get; set; } = default!;
    public FamilyUnit NewFamily { get; set; } = new();
    public FamilyMember CurrentUser { get; set; } = default!;
    public List<FamilyUnit> Neighborhood { get; set; } = new();
}

