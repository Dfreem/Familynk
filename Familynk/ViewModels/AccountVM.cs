namespace Familynk.ViewModels;

public class AccountVM
{
    public string? CurrentUserId { get; set; } = default!;
    public string FamilyName { get; set; } = "No Family";
    [Required(ErrorMessage = "must enter a user name")]
    [MaxLength(20, ErrorMessage = "user name must be less than 20 characters")]
    public string? UserName { get; set; } = default!;
    public string Name { get; set; } = default!;
    [MaxLength(20)]
    [MinLength(8)]
    [PasswordPropertyText]
    public string CurrentPassword { get; set; } = default!;
    [MaxLength(20)]
    [MinLength(8)]
    [PasswordPropertyText]
    [Compare(nameof(CurrentPassword), ErrorMessage = "Passwords must match")]
    public string ConfirmPassword { get; set; } = default!;
    [MaxLength(20)]
    [MinLength(8)]
    [PasswordPropertyText]
    public string NewPassword { get; set; } = default!;
    [EmailAddress]
    public string? Email { get; set; }
    [Url]
    public string ReturnUrl { get; set; } = "/";
    public string? PhoneNumber { get; set; }
    public bool RememberMe { get; set; } = true;
    public AccountVM()
    {
        Name = "Name";
    }
    public AccountVM(FamilyMember current)
    {
        CurrentUserId = current.Id;
        UserName = current.UserName;
        Name = current.Name;
        Email = current.Email;
        PhoneNumber = current.PhoneNumber;
    }

    public static explicit operator FamilyMember(AccountVM avm)
    {
        var newMember = new FamilyMember()
        {
            Name = avm.Name,
            UserName = avm.UserName,
            Email = avm.Email,
            PhoneNumber = avm.PhoneNumber,
            Id = avm.CurrentUserId ?? ""
        };
        if (avm.FamilyName is not null)
        {
            newMember.Roles.Add(avm.FamilyName);
        }
        return newMember;
    }
}

