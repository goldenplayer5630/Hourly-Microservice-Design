using Hourly.GitService.Domain.Entities;
using Hourly.GitService.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.GitService.Infrastructure.Persistence.EntityConfigurations
{
    public class GitCommitConfiguration : IEntityTypeConfiguration<GitCommit>
    {
        public void Configure(EntityTypeBuilder<GitCommit> builder)
        {
            builder.ToTable("git_commits");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.ExtCommitId)
                .IsRequired()
                .HasColumnName("ext_commit_id");

            builder.Property(x => x.ExtCommitShortId)
                .HasMaxLength(50)
                .HasColumnName("ext_commit_short_id");

            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            builder.Property(x => x.Comment)
                .HasColumnName("comment");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter)
                .HasColumnName("created_at");

            builder.Property(x => x.UpdatedAt)
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter)
                .HasColumnName("updated_at");

            builder.Property(x => x.AuthoredDate)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter)
                .HasColumnName("authored_date");

            builder.Property(x => x.WebUrl)
                .HasMaxLength(500)
                .HasColumnName("web_url");

            builder.Property(x => x.AuthorId)
                .HasColumnName("author_id");

            builder.Property(x => x.GitRepositoryId)
                .HasColumnName("git_repository_id");

            builder.HasOne(x => x.GitRepository)
                .WithMany(r => r.GitCommits)
                .HasForeignKey(x => x.GitRepositoryId);
        }
    }
}
