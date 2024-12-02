using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ServerLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User Entity Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.Username)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(u => u.Email)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.Property(u => u.Role)
                    .HasMaxLength(20)
                    .IsRequired();

                // Seed Admin User
                entity.HasData(new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                    Role = "Admin"
                });
            });

            // Recipe Entity Configuration
            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasOne(r => r.User)
                    .WithMany(u => u.Recipes) // Added navigation property for clarity
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(r => r.Title)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(r => r.Description)
                    .HasMaxLength(1000)
                    .IsRequired();

                entity.Property(r => r.Category)
                    .HasMaxLength(50); // Optional category for simplicity

                entity.Property(r => r.Ingredients)
                    .HasMaxLength(1000); // Allow a reasonable size for ingredients list

                entity.Property(r => r.Instructions)
                    .HasMaxLength(2000); // Allow detailed instructions
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Ignore Pending Model Changes Warning
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
