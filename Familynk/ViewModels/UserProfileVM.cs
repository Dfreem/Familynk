namespace Familynk.ViewModels;

public class UserProfileVM
{
    [Required]
    public FamilyMember GetMember { get; set; } = default!;
    [PasswordPropertyText]
    public string Password { get; set; } = default!;
    [PasswordPropertyText]
    public string NewPassword { get; set; } = default!;
    [PasswordPropertyText]
    public string ConfirmPassword { get; set; } = default!;
    [Required]
    public string Id { get; set; } = "";
    [Required]
    [MaxLength(20, ErrorMessage = "First Name should be less than 20 characters")]
    public string Name { get; set; } = default!;
    [EmailAddress]
    public string? Email { get; set; } = default!;
    [MinLength(8, ErrorMessage = "UserName must be atleast 8 characters long")]
    [MaxLength(20, ErrorMessage = "UserName cannot be longer than 20 characters")]
    [Required]
    public string UserName { get; set; } = "No UserName";
    public DateOnly Birthday { get; set; } = default!;
    [Phone]
    public string? PhoneNumber { get; set; } = default!;
    public UserProfileVM()
    {

    }

    public UserProfileVM(FamilyMember fMember)
    {
        GetMember = fMember;
        Id = fMember.Id;
        Name = fMember.Name;
        Email = fMember.Email;
    }

    public static explicit operator FamilyMember(UserProfileVM upv)
    {
        return upv.GetMember;
    }

}

