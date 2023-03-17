using System;
namespace Familynk.ViewModels
{
    public class RegisterVM 
    {
        public string Name { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        public string Email { get; internal set; }
        public DateOnly Birthday { get; internal set; }
        public string? PhoneNumber { get; internal set; }

        public static explicit operator FamilyMember(RegisterVM v)
        {
            return new FamilyMember()
            {
                Name = v.Name,
                UserName = v.UserName,
                LockoutEnabled = false,
            };
        }
    }
}

