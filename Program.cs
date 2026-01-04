using E_project2025;
using E_project2025.Areas.Identity.Data;
using E_project2025.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
var connectionString = builder.Configuration.GetConnectionString("E_project2025ContextConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'E_project2025ContextConnection' not found."
    );

builder.Services.AddDbContext<E_project2025Context>(options =>
    options.UseSqlServer(connectionString)
);

// Identity
builder.Services.AddIdentity<E_project2025User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<E_project2025Context>()
.AddDefaultTokenProviders();





// External authentication ONLY
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["GoogleKeys:ClientId"];
        options.ClientSecret = builder.Configuration["GoogleKeys:Clientsecret"];
        options.CallbackPath = "/signin-google";
    })
    .AddGitHub(options =>
    {
        options.ClientId = builder.Configuration["GitHubKeys:ClientId"];
        options.ClientSecret = builder.Configuration["GitHubKeys:ClientSecret"];
        options.CallbackPath = "/signin-github";
    });

// MVC + Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<UserAnalyticsService>();

var app = builder.Build();

// ================= AUTO ROLE CREATION (ONLY ADDITION) =================
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "Faculty", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
// =====================================================================

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();

app.Run();
