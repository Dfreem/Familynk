using System;
using System.ComponentModel;

namespace Familynk.ViewModels;

public class LoginVM
{
    [Required]
    [MaxLength(20, ErrorMessage = "User Name should be less than 20 characters.")]
    public string UserName { get; set; } = default!;
    [Required]
    [PasswordPropertyText]
    [MaxLength(20, ErrorMessage = "Password should be less than 20 characters.")]
    public string Password { get; set; } = default!;
    public bool RememberMe { get; set; } = false;
    [Required(AllowEmptyStrings = true)]
    public string ReturnUrl { get; set; } = "/";
}

