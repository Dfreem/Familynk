using System;
namespace Familynk.ViewModels
{
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
        public DateTime? Birthday { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = default!;
        [Required]
        [PasswordPropertyText]
        public string ConfirmPassword { get; set; } = default!;
    }
}

