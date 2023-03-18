namespace Familynk.Data;

public static class Seed
{
    public static async Task SeedUsersAsync(IServiceProvider services)
    {

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<FamilyMember>>();
        var context = services.GetRequiredService<FamilyContext>();
        if (userManager.Users.Any()) { return; }
        FamilyUnit freemanFam = new() { FamilyName = "Freeman" };
        await context.Neighborhood.AddAsync(freemanFam);
        await context.SaveChangesAsync();
        freemanFam = await context.Neighborhood.FirstAsync(n => n.FamilyName.Equals("Freeman"));

        FamilyMember devin = new()
        {
            Birthday = new(1987, 9, 30),
            Email = "dfreem987@gmail.com",
            Name = "Devin",
            UserName = "dfreem987",
            PhoneNumber = "541-525-8040",
            Family = freemanFam
        };
        FamilyMember testMember = new()
        {
            Name = "unit member 1",
            Birthday = DateTime.Now,
            Email = "test@test.com",
            EmailConfirmed = true,
            PhoneNumber = "555-555-5555",
            UserName = "TestUser123",
            Family = freemanFam
        };
        var result = await userManager.CreateAsync(devin!, "!BassCase987");
        var result2 = await userManager.CreateAsync(testMember, "@Password123");
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
        await userManager.AddToRoleAsync(testMember, "FamilyMember");
    }
}
