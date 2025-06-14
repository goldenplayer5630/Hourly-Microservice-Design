using Hourly.GitService.Domain.Entities;
using Hourly.GitService.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.GitService.Infrastructure.Persistence.EntityConfigurations
{
    public class GitRepositoryConfiguration : IEntityTypeConfiguration<GitRepository>
    {
        public void Configure(EntityTypeBuilder<GitRepository> builder)
        {
            builder.ToTable("git_repository");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name");

            builder.Property(x => x.ExtRepositoryId)
                .IsRequired()
                .HasColumnName("ext_repository_id");

            builder.Property(x => x.Namespace)
                .HasMaxLength(255)
                .HasColumnName("namespace");

            builder.Property(x => x.WebUrl)
                .HasMaxLength(500)
                .HasColumnName("web_url");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at")
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);
        }
    }
}
