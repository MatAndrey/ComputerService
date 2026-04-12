using ComputerService.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPrivilege> UserPrivileges { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SupportMessage> SupportMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPrivilege>()
                .HasKey(up => new { up.UserLogin, up.PrivilegeName });

            base.OnModelCreating(modelBuilder);
        }
    }
}
