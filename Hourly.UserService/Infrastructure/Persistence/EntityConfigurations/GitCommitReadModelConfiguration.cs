using Hourly.UserService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.UserService.Infrastructure.Persistence.EntityConfigurations
{
    public class GitCommitReadModelConfiguration : IEntityTypeConfiguration<GitCommitReadModel>
    {
        public void Configure(EntityTypeBuilder<GitCommitReadModel> builder)
        {
            builder.ToTable("git_commit_read_model");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.AuthorId).HasColumnName("author_id");

            builder.HasOne(x => x.Author)
                .WithMany(u => u.GitCommits)
                .HasForeignKey(x => x.AuthorId)
                .HasConstraintName("fk_git_commit_read_model_author");
        }
    }
}
