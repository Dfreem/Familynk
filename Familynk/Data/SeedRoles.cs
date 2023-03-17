namespace Familynk.Data;

public static class SeedRoles
{
    public static async Task SeedUsersAsync(IServiceProvider services)
    {

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<FamilyMember>>();
        var devin = new FamilyMember()
        {
            Birthday = new(1987, 9, 30),
            Email = "dfreem987@gmail.com",
            Name = "Devin",
            UserName = "dfreem987",
            FamilyName = "Freeman",
            PhoneNumber = "541-525-8040"
        };
        await userManager.CreateAsync(devin!, "!BassCase987");
        if (await roleManager.FindByNameAsync("FamilyMember") is null)
        {
            await roleManager.CreateAsync(new IdentityRole("FamilyMember"));
        }

        if (await roleManager.FindByNameAsync("HOH") is null)
        {
            await roleManager.CreateAsync(new IdentityRole("HOH"));
        }
        
        await userManager.AddToRoleAsync(devin!, "FamilyMember");
        await userManager.AddToRoleAsync(devin!, "HOH");
    }
}
