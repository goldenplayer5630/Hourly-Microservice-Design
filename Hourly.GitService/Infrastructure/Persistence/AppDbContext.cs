using Hourly.GitService.Domain.Entities;
using Hourly.GitService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<GitRepository> GitRepositories { get; set; }
    public DbSet<GitCommit> GitCommits { get; set; }
    public DbSet<UserReadModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

}
