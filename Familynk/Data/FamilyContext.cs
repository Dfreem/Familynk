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

    public DbSet<DirectMessage> DMs { get; set; }
    public DbSet<FamilyEvent> Events { get; set; }
    public DbSet<FamilyCalendar> FamilyCalendars { get; set; }
    public DbSet<FamilyMessage> ChatTv { get; set; }
    public DbSet<FamilyUnit> Neighborhood { get; set; }
    public DbSet<HouseRules> Rules { get; set; }
    public DbSet<MagneticMessage> Refrigerator { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Scrap> Scraps { get; set; }
    public DbSet<ScrapBook> ScrapBooks { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet <UserSettings> UserSettings { get; set; }

}

