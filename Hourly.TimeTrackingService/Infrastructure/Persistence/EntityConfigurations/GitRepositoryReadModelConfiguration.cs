using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence.EntityConfigurations
{
    public class GitRepositoryReadModelConfiguration : IEntityTypeConfiguration<GitRepositoryReadModel>
    {
        public void Configure(EntityTypeBuilder<GitRepositoryReadModel> builder)
        {
            builder.ToTable("git_repository_read_model");

            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.ExtRepositoryId)
                .HasColumnName("ext_repository_id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(x => x.WebUrl)
                .HasColumnName("web_url")
                .IsRequired();

            // One-to-many: GitRepository -> GitCommits
            builder
                .HasMany<GitCommitReadModel>()
                .WithOne()
                .HasForeignKey("RepositoryId")
                .HasConstraintName("fk_git_commit_repository")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
