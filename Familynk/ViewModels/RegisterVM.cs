using System;
namespace Familynk.ViewModels;

public class RegisterVM
{
    [Required]
    [MinLength(1, ErrorMessage = "name cannot be empty")]
    [MaxLength(20, ErrorMessage = "Name should be less than 20 characters")]
    public string Name { get; set; } = default!;
    [Required]
    public string UserName { get; set; } = default!;
    [EmailAddress]
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = default!;
    [Required]
    [PasswordPropertyText]
    [Compare("Password", ErrorMessage = "passwords must match")]
    public string ConfirmPassword { get; set; } = default!;
    public FamilyUnit? Family { get; set; } = default!;

    /// <summary>
    /// The programmer should be aware that the FamilyMember returned by this operator is unregistered, does not belong to a family unit and has no id's.
    /// </summary>
    /// <param name="rvm">the view model that is carrying the entity propertes</param>
    public static explicit operator FamilyMember(RegisterVM rvm)
    {
        return new FamilyMember()
        {
            Name = rvm.Name,
            UserName = rvm.UserName,
            Email = rvm.Email,
            PhoneNumber = rvm.PhoneNumber,
            FamilyUnitId = rvm.Family?.FamilyUnitId
        };
    }
}