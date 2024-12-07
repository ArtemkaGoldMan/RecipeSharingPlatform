using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;

namespace ServerLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets for all entities
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeDetails> RecipeDetails { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Recipe entity configuration
            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(r => r.Title).HasMaxLength(100).IsRequired();
                entity.Property(r => r.Description).HasMaxLength(1000).IsRequired();
                entity.Property(r => r.Creator).HasMaxLength(50).IsRequired(); // Creator is required
                entity.Property(r => r.Category).HasMaxLength(50);
                entity.Property(r => r.Ingredients).HasMaxLength(1000);
                entity.Property(r => r.Instructions).HasMaxLength(2000);

                // 1:1 relationship with RecipeDetails
                entity.HasOne(r => r.RecipeDetails)
                      .WithOne(rd => rd.Recipe)
                      .HasForeignKey<RecipeDetails>(rd => rd.RecipeId)
                      .OnDelete(DeleteBehavior.Cascade);

                // 1:* relationship with Comments
                entity.HasMany(r => r.Comments)
                      .WithOne(c => c.Recipe)
                      .HasForeignKey(c => c.RecipeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // RecipeDetails entity configuration
            modelBuilder.Entity<RecipeDetails>(entity =>
            {
                entity.Property(rd => rd.NutritionInfo).HasMaxLength(500);
                entity.Property(rd => rd.PreparationTime).HasMaxLength(100);
            });

            // Comment entity configuration
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(c => c.Text).HasMaxLength(1000).IsRequired();
                entity.Property(c => c.Author).HasMaxLength(50).IsRequired();
            });

            // Tag entity configuration
            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(t => t.Name).HasMaxLength(50).IsRequired();
            });

            // RecipeTag (many-to-many relationship) configuration
            modelBuilder.Entity<RecipeTag>(entity =>
            {
                entity.HasKey(rt => new { rt.RecipeId, rt.TagId }); // Composite key

                entity.HasOne(rt => rt.Recipe)
                      .WithMany(r => r.RecipeTags)
                      .HasForeignKey(rt => rt.RecipeId);

                entity.HasOne(rt => rt.Tag)
                      .WithMany(t => t.RecipeTags)
                      .HasForeignKey(rt => rt.TagId);
            });
        }
    }
}
