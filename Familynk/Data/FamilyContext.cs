namespace Familynk.Data;

public class FamilyContext : IdentityDbContext<FamilyMember>
{
    public FamilyContext(DbContextOptions<FamilyContext> options)
        : base(options)
    {
    }
    public FamilyContext()
    {

    }
    public DbSet<FamilyUnit> Neighborhood { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Image> Images { get; set; }

}

