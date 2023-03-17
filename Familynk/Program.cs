

var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Critical).ClearProviders();
//builder.Configuration.AddUserSecrets("aspnet-FamilyChat-7d98e67b-94f6-4133-b439-a9c553dddc21");
string connection = builder.Configuration.GetConnectionString("AZURE_MYSQL_CONNECTION")!;

// Add services to the container.
builder.Services.AddDbContext<FamilyContext>(options =>
    options.UseMySql(connection, MySqlServerVersion.Parse("mysql-8.0")));
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 3;
    config.Position = NotyfPosition.TopRight;
    config.HasRippleEffect = true;
    config.IsDismissable = true;
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
//builder.Services
//    .AddNotyf(config =>
//    {
//        config.DurationInSeconds = 6;
//        config.IsDismissable = true;
//        config.Position = NotyfPosition.TopRight;
//    });
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FamilyContext>();
    //Init in the static SeedData class checks for the presence of data in the database before seeding or returning.
    await SeedRoles.SeedUsersAsync(services);
}

app.Run();