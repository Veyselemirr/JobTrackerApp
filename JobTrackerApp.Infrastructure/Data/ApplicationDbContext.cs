using JobTrackerApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet: Veritabanında tablolara karşılık gelen sınıflar
        public DbSet<User> Users => Set<User>();  // Users tablosu
        public DbSet<JobApplication> JobApplications => Set<JobApplication>();  // JobApplications tablosu

        // Model yaratılırken özel ayarlar yapılabilir (İlişkiler, kısıtlamalar vs.)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Örnek: Email alanını unique yapabiliriz (ileride Fluent API ile eklenir)
            modelBuilder.Entity<User>()
                        .HasIndex(u => u.Email)
                        .IsUnique();
        }
    }
}
