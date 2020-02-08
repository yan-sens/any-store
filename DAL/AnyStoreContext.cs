using DAL.Models;
using DAL.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AnyStoreContext : IdentityDbContext<ApplicationUser>
    {
        public AnyStoreContext(DbContextOptions<AnyStoreContext> options) : base(options) { }

        public AnyStoreContext() { }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(x => new { x.Id });

            base.OnModelCreating(modelBuilder);
        }
    }
}
