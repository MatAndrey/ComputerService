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
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Login);
                entity.Property(e => e.Login).HasColumnName("login");
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
                entity.Property(e => e.Email).HasColumnName("email");
            });

            modelBuilder.Entity<UserPrivilege>(entity =>
            {
                entity.ToTable("user_privileges");
                entity.HasKey(e => new { e.UserLogin, e.PrivilegeName });
                entity.Property(e => e.UserLogin).HasColumnName("user_login");
                entity.Property(e => e.PrivilegeName).HasColumnName("privilege_name");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.UserPrivileges)
                      .HasForeignKey(e => e.UserLogin)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("pages");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<PageTranslation>(entity =>
            {
                entity.ToTable("page_translations");
                entity.HasKey(e => new { e.PageId, e.LangCode });
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Content).HasColumnName("content");
                entity.Property(e => e.LangCode).HasColumnName("lang_code");
                entity.Property(e => e.PageId).HasColumnName("page_id");

                entity.HasOne(e => e.Page)
                      .WithMany(p => p.Translations)
                      .HasForeignKey(e => e.PageId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("news");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Date).HasColumnName("date").HasColumnType("date");
                entity.Property(e => e.Image).HasColumnName("image");
            });

            modelBuilder.Entity<NewsTranslation>(entity =>
            {
                entity.ToTable("news_translations");
                entity.HasKey(e => new { e.NewsId, e.LangCode });
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Content).HasColumnName("content");
                entity.Property(e => e.LangCode).HasColumnName("lang_code");
                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.HasOne(e => e.News)
                      .WithMany(n => n.Translations)
                      .HasForeignKey(e => e.NewsId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<CategoryTranslation>(entity =>
            {
                entity.ToTable("category_translations");
                entity.HasKey(e => new { e.CategoryId, e.LangCode });
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.LangCode).HasColumnName("lang_code");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Translations)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(10,2)");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.Visible).HasColumnName("visible");

                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductTranslation>(entity =>
            {
                entity.ToTable("product_translations");
                entity.HasKey(e => new { e.ProductId, e.LangCode });
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.LangCode).HasColumnName("lang_code");
                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(e => e.Product)
                      .WithMany(p => p.Translations)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("product_images");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.ImageUrl).HasColumnName("image_url");

                entity.HasOne(e => e.Product)
                      .WithMany(p => p.Images)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Date).HasColumnName("date").HasColumnType("timestamp without time zone");
                entity.Property(e => e.CustomerName).HasColumnName("customer_name");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
                entity.Property(e => e.DeliveryMethod).HasColumnName("delivery_method");
                entity.Property(e => e.DeliveryAddress).HasColumnName("delivery_address");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.Total).HasColumnName("total").HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_items");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(10,2)");

                entity.HasOne(e => e.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(e => e.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                      .WithMany(p => p.OrderItems)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("reviews");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AuthorName).HasColumnName("author_name");
                entity.Property(e => e.Date).HasColumnName("date").HasColumnType("timestamp without time zone");
                entity.Property(e => e.Text).HasColumnName("text");
                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.ToTable(t => t.HasCheckConstraint("CK_Review_Rating", "\"rating\" BETWEEN 1 AND 5"));
            });

            modelBuilder.Entity<SupportMessage>(entity =>
            {
                entity.ToTable("support_messages");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Message).HasColumnName("message");
                entity.Property(e => e.Date).HasColumnName("date").HasColumnType("timestamp without time zone");
            });
        }
    }
}
