using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL
{
    public class AnyStoreContextFactory : IDesignTimeDbContextFactory<AnyStoreContext>
    {
        AnyStoreContext IDesignTimeDbContextFactory<AnyStoreContext>.CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AnyStoreContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AnyStoreDB"), sqlServerOptions => sqlServerOptions.CommandTimeout(600));

            return new AnyStoreContext(optionsBuilder.Options);
        }
    }
}
