using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_project2025.Data;
using E_project2025.Areas.Identity.Data;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("E_project2025ContextConnection") ?? throw new InvalidOperationException("Connection string 'E_project2025ContextConnection' not found.");;

builder.Services.AddDbContext<E_project2025Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<E_project2025User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<E_project2025Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapRazorPages();

app.Run();
