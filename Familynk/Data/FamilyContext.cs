using Notification = Familynk.Models.Messages.Notification;

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
    public DbSet<FamilyEvent> Events { get; set; }
    public DbSet<FamilyCalendar> FamilyCalendars { get; set; }
    public DbSet<FamilyUnit> Neighborhood { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Scrap> Scraps { get; set; }
    public DbSet<Image> Images { get; set; }

}

