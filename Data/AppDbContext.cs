using ComputerService.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserPrivilege> UserPrivileges { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageTranslation> PageTranslations { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsTranslation> NewsTranslations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SupportMessage> SupportMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PageTranslation>()
                .HasKey(pt => new { pt.PageId, pt.LangCode });

            modelBuilder.Entity<NewsTranslation>()
                .HasKey(nt => new { nt.NewsId, nt.LangCode });

            modelBuilder.Entity<CategoryTranslation>()
                .HasKey(ct => new { ct.CategoryId, ct.LangCode });

            modelBuilder.Entity<ProductTranslation>()
                .HasKey(pt => new { pt.ProductId, pt.LangCode });

            modelBuilder.Entity<UserPrivilege>()
                .HasKey(up => new { up.UserLogin, up.PrivilegeName });

            modelBuilder.Entity<UserPrivilege>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPrivileges)
                .HasForeignKey(up => up.UserLogin)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .ToTable(t => t.HasCheckConstraint("CK_Review_Rating", "\"Rating\" BETWEEN 1 AND 5"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
