using Common.Enums;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace DAL
{
    public class AnyStoreInitializer
    {
        public void Initialize(AnyStoreContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Roles.Any()) { SeedRoles(context); }
        }

        private async void SeedRoles(AnyStoreContext context)
        {
            context.Roles.AddRange(new IdentityRole { Name = RolesEnum.Administrator.ToString(), NormalizedName = RolesEnum.Administrator.ToString().ToUpper() });

            await context.SaveChangesAsync();
        }
    }
}
