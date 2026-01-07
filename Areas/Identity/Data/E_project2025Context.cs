using E_project2025.Areas.Identity.Data;
using E_project2025.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_project2025.Data;

public class E_project2025Context : IdentityDbContext<E_project2025User>
{
    public E_project2025Context(DbContextOptions<E_project2025Context> options)
        : base(options)
    {
    }
    public DbSet<Survay> Survays { get; set; }
    public DbSet<Question> Questions { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
