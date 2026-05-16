using CodePulse.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.Infrastructure.Data
{
    public class AppDBContext : IdentityDbContext<IdentityUser>
    {

        public AppDBContext(DbContextOptions option) : base(option)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogCategory> Categorires { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // ─── BlogCategory configuration ───
            modelBuilder.Entity<BlogCategory>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UrlHandle)
                    .IsRequired()
                    .HasMaxLength(200);

                // Unique index on UrlHandle — no two categories can share the same URL
                entity.HasIndex(e => e.UrlHandle)
                    .IsUnique();

                // Unique index on Name
                entity.HasIndex(e => e.Name)
                    .IsUnique();
            });

            // ─── BlogPost configuration ───
            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(500);

                entity.Property(e => e.Content)
                    .IsRequired();

                entity.Property(e => e.UrlHandle)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FeatureImageUrl)
                    .HasMaxLength(500);

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(100);

                // Unique index on UrlHandle — no two posts can share the same URL
                entity.HasIndex(e => e.UrlHandle)
                    .IsUnique();
            
            /// ─── Relationships ───
            /// 
            
            entity.HasOne(p => p.Category)          // BlogPost has one BlogCategory
                  .WithMany(c => c.Posts)            // BlogCategory has many BlogPosts
                  .HasForeignKey(p => p.CategoryId)  // Foreign key is CategoryId
                  .OnDelete(DeleteBehavior.Restrict); // Prevent deleting category with posts
            });
        }
    }
}
