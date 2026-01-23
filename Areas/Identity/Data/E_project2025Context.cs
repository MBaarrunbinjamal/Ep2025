using E_project2025.Areas.Identity.Data;
using E_project2025.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_project2025.Data;

public class E_project2025Context : IdentityDbContext<E_project2025User>
{
    internal object seminars;

    public E_project2025Context(DbContextOptions<E_project2025Context> options)
        : base(options)
    {
    }

    public DbSet<Survay> Survays { get; set; }
    public DbSet<Question> Questions { get; set; }
<<<<<<< HEAD
=======
  
>>>>>>> bd9066fd25afaa6bcccdc7b5e5bcc5c465bd5f3b
    public DbSet<Answers> Answers { get; set; }

    // ✅ Add ContactModel DbSet here
    public DbSet<ContactModel> ContactModels { get; set; }

    public DbSet<seminar> seminar { get; set; }
    public DbSet<SeminarRegistration> SeminarRegistrations { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Survey → Answers
        builder.Entity<Answers>()
            .HasOne(a => a.survey)
            .WithMany()
            .HasForeignKey(a => a.SurveyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Question → Answers
        builder.Entity<Answers>()
            .HasOne(a => a.questions)
            .WithMany()
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        // User → Answers
        builder.Entity<Answers>()
            .HasOne(a => a.users)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
