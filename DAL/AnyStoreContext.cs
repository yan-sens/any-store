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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(x => new { x.Id });

            modelBuilder.Entity<Category>().HasMany(c => c.Categories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
