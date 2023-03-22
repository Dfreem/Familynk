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
            Id = "d1",
            Birthday = new(1987, 9, 30),
            Email = "dfreem987@gmail.com",
            Name = "Devin",
            UserName = "dfreem987",
            PhoneNumber = "123-456-7890",
            FamilyUnitId = freemanFam.FamilyUnitId
        };
        FamilyMember testMember = new()
        {
            Id = "t1",
            Name = "some guy",
            Birthday = DateTime.Now,
            Email = "test@test.com",
            EmailConfirmed = true,
            PhoneNumber = "555-555-5555",
            UserName = "TestUser123",
            FamilyUnitId = freemanFam.FamilyUnitId
        };
        var result = await userManager.CreateAsync(devin!, "!BassCase987");
        var result2 = await userManager.CreateAsync(testMember, "@Password123");
        if (result.Succeeded) { freemanFam.Members.Add(devin); }
        if (result2.Succeeded) { freemanFam.Members.Add(testMember); }

        if (await roleManager.FindByNameAsync("FamilyMember") is null)
        {
            await roleManager.CreateAsync(new IdentityRole("FamilyMember"));
        }

        if (await roleManager.FindByNameAsync("HOH") is null)
        {
            await roleManager.CreateAsync(new IdentityRole("HOH"));
        }
        context.Neighborhood.Update(freemanFam);
        context.SaveChanges();
        await userManager.AddToRoleAsync(devin!, "FamilyMember");
        await userManager.AddToRoleAsync(devin!, "HOH");
        await userManager.AddToRoleAsync(testMember, "FamilyMember");

        SeedChat(services);
    }
    public static void SeedChat(IServiceProvider services)
    {
        var context = services.GetRequiredService<FamilyContext>();
        if (!context.ChatTv.IsNullOrEmpty())
        { return; }
        context.ChatTv.Add(new()
        {
            Body = "This is a new chat message",
            SenderName = "Devin",
            Family = context.Neighborhood.First(u => u.FamilyName.Equals("Freeman"))
        });

        context.ChatTv.Add(new()
        {
            Body = "Ok, sounds good",
            SenderName = "some guy",
            Family = context.Neighborhood.First(u => u.FamilyName.Equals("Freeman"))
        });

        context.ChatTv.Add(new()
        {
            Body = "This is cool",
            SenderName = "Devin",
            Family = context.Neighborhood.First(u => u.FamilyName.Equals("Freeman"))
        });
        context.SaveChanges();

        
    }

    public async static Task SeedDms(IServiceProvider services)
    {
        var context = services.GetRequiredService<FamilyContext>();

        if (!context.DMs.IsNullOrEmpty())
        { return; }
        context.DMs.Add(new()
        {
            Body = " This is a test message",
            RecipientId = "d1",
            SenderId = "t1"
        });
        await context.SaveChangesAsync();
    }

    public static void SeedCalendar(IServiceProvider services)
    {
        FamilyContext context = services.GetRequiredService<FamilyContext>();
        var family = context.Neighborhood.First(f => f.FamilyName.Equals("Freeman"));
        family.GetCalendar.Events?.Add(new()
        {
            Details = "this is for testing",
            Title = "Test Event",
            EventDate = DateTime.Now,
            SenderId = "d1"
        });
        context.Neighborhood.Update(family);
        context.SaveChanges();
    }
    
}
