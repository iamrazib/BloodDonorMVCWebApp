using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BloodDonorMVCWebApp.Data
{
    public class BloodDonorDbContextFactory: IDesignTimeDbContextFactory<BloodDonorDbContext>
    {
        public BloodDonorDbContext CreateDbContext(string[] args)
        {
            // Load configuration from appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<BloodDonorDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new BloodDonorDbContext(optionsBuilder.Options);
        }
    }
}
