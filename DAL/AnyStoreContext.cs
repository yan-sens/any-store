using DAL.Models;
using DAL.Models.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AnyStoreContext : IdentityDbContext<ApplicationUser>
    {
        public AnyStoreContext(DbContextOptions<AnyStoreContext> options) : base(options) { }

        public AnyStoreContext() { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Currency> Currencies { get; set; }  
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionMapping> PromotionMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Category>().HasMany(c => c.Categories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId);

            modelBuilder.Entity<ProductImage>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Product>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Product>().HasMany(c => c.Images)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Currency)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CurrencyId);

            modelBuilder.Entity<Promotion>()
                .HasMany(x => x.ProductMappings)
                .WithOne(x => x.Promotion)
                .HasForeignKey(x => x.PromotionId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
