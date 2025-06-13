using Microsoft.EntityFrameworkCore;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Manually set the base path to the Runtime project where appsettings.json lives
            var basePath = Path.Combine(Directory.GetCurrentDirectory());

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
