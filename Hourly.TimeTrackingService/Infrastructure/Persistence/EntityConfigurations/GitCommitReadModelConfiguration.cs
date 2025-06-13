using Hourly.TimeTrackingService.Domain.Entities;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence.EntityConfigurations
{
    public class GitCommitReadModelConfiguration : IEntityTypeConfiguration<GitCommitReadModel>
    {
        public void Configure(EntityTypeBuilder<GitCommitReadModel> builder)
        {
            builder.ToTable("git_commit_read_model");

            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            // Properties

            builder.Property(x => x.ExtCommitId).HasColumnName("ext_commit_id").IsRequired();
            builder.Property(x => x.ExtCommitShortId).HasColumnName("ext_commit_short_id").IsRequired();
            builder.Property(x => x.Title).HasColumnName("title").IsRequired().HasMaxLength(255);
            builder.Property(x => x.WebUrl).HasColumnName("web_url").IsRequired().HasMaxLength(500);
            builder.Property(x => x.AuthoredDate).HasColumnName("authored_date").IsRequired();

            // Author relation
            builder.Property(x => x.AuthorId).HasColumnName("author_id").IsRequired();

            builder.HasOne(x => x.Author)
                .WithMany()
                .HasForeignKey(x => x.AuthorId)
                .HasConstraintName("fk_git_commit_author")
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete of user

            // Repository relation
            builder.Property(x => x.RepositoryId).HasColumnName("repository_id").IsRequired();
            builder.HasOne(x => x.Repository)
                .WithMany()
                .HasForeignKey(x => x.RepositoryId)
                .HasConstraintName("fk_git_commit_repository")
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete of repository

            // WorkSession many-to-many via join table
            builder
                .HasMany(x => x.WorkSessions)
                .WithMany(x => x.GitCommits)
                .UsingEntity<Dictionary<string, object>>(
                    "work_session_git_commit",
                    join => join
                        .HasOne<WorkSession>()
                        .WithMany()
                        .HasForeignKey("work_session_id")
                        .HasConstraintName("fk_wsgc_work_session")
                        .OnDelete(DeleteBehavior.Cascade),
                    join => join
                        .HasOne<GitCommitReadModel>()
                        .WithMany()
                        .HasForeignKey("git_commit_id")
                        .HasConstraintName("fk_wsgc_git_commit")
                        .OnDelete(DeleteBehavior.Cascade),
                    join =>
                    {
                        join.ToTable("work_session_git_commit");
                        join.HasKey("work_session_id", "git_commit_id");
                    });
        }
    }
}