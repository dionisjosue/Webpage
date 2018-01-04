using BuyWebPage.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BuyWebPage
{
    public class DesingTimeFactory : IDesignTimeDbContextFactory<BuyDbContext>
    {
        public BuyDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("config.json", false, true)
            .Build();
            var builder = new DbContextOptionsBuilder<BuyDbContext>();
            var connectionString = configuration.GetConnectionString("BuyConnectionString");
            builder.UseSqlServer(connectionString);
            return new BuyDbContext(builder.Options);
        }
    }
}
