using Hourly.UserService.Domain.Entities;
using Hourly.UserService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Hourly.UserService.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<UserContract> UserContracts { get; set; }
        public DbSet<GitCommitReadModel> GitCommits { get; set; }
        public DbSet<WorkSessionReadModel> WorkSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
