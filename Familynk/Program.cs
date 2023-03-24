
var builder = WebApplication.CreateBuilder(args);

builder.Logging.SetMinimumLevel(LogLevel.Debug).ClearProviders();
string connection = builder.Configuration.GetConnectionString("AZURE_CONNECTION")!;

// Add services to the container.
builder.Services.AddDbContext<FamilyContext>(options =>
{
    options.UseMySql(connection, MySqlServerVersion.Parse("mysql-8.0"));
    options.EnableSensitiveDataLogging();
});
//builder.Services.AddTransient<ISiteRepository, SiteRepository>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddHttpClient();
// This call adds a Role manager to the services container.
builder.Services.AddIdentity<FamilyMember, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = false)
        //.AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<FamilyContext>()
        .AddDefaultTokenProviders();

builder.Services.AddSingleton<IFamilyRepo,FakeFamilyRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 3;
    config.Position = NotyfPosition.TopRight;
    config.HasRippleEffect = true;
    config.IsDismissable = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseResponseCaching();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseNotyf();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
// use scoped service provider to call SeedData initialization.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    //Init in the static SeedData class checks for the presence of data in the database before seeding or returning.
    Seed.SeedUsersAsync(services).Wait();
    Seed.SeedChat(services);
    Seed.SeedDms(services).Wait();
    Seed.SeedCalendar(services);
}

app.Run();