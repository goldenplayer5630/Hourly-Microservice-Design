using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence.EntityConfigurations
{
    public class UserReadModelConfiguration : IEntityTypeConfiguration<UserReadModel>
    {
        public void Configure(EntityTypeBuilder<UserReadModel> builder)
        {
            builder.ToTable("user_read_model");

            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            // Name
            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            // TVT hour balance
            builder.Property(x => x.TVTHourBalance)
                .HasColumnName("tvt_hour_balance")
                .IsRequired();

            // One-to-many: User → GitCommits
            builder.HasMany(x => x.GitCommits)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId)
                .HasConstraintName("fk_git_commit_user")
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many: User → Contracts
            builder.HasMany(x => x.Contracts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("fk_user_contract_user")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}