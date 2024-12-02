using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;

namespace ServerLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(r => r.Title).HasMaxLength(100).IsRequired();
                entity.Property(r => r.Description).HasMaxLength(1000).IsRequired();
                entity.Property(r => r.Creator).HasMaxLength(50).IsRequired(); // Creator is required
                entity.Property(r => r.Category).HasMaxLength(50);
                entity.Property(r => r.Ingredients).HasMaxLength(1000);
                entity.Property(r => r.Instructions).HasMaxLength(2000);
            });
        }
    }
}
