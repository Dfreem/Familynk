

namespace Familynk.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<DirectMessage> DMs { get; set; }
    public DbSet<FamilyMember> FamilyMembers { get; set; }
    public DbSet<FamilyUnit> Neighborhood { get; set; }
    public DbSet<HouseRules> MyProperty { get; set; }
    public DbSet<Scrap> Scraps { get; set; }
    public DbSet<ScrapBook> AllBooks { get; set; }

}

