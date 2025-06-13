using Hourly.TimeTrackingService.Domain.Entities;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<UserReadModel> Users { get; set; }
        public DbSet<UserContractReadModel> UserContracts { get; set; }
        public DbSet<GitRepositoryReadModel> GitRepositories { get; set; }
        public DbSet<GitCommitReadModel> GitCommits { get; set; }
        public DbSet<WorkSession> WorkSessions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
