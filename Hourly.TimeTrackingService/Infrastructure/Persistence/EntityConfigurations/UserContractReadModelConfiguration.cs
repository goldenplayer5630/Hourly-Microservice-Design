using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence.EntityConfigurations
{
    public class UserContractReadModelConfiguration : IEntityTypeConfiguration<UserContractReadModel>
    {
        public void Configure(EntityTypeBuilder<UserContractReadModel> builder)
        {
            builder.ToTable("user_contract_read_model");

            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            // Required properties
            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            // Foreign key to UserReadModel
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("fk_user_contract_user")
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many: UserContract → WorkSessions
            builder.HasMany(x => x.WorkSessions)
                .WithOne(x => x.UserContract)
                .HasForeignKey(x => x.UserContractId)
                .HasConstraintName("fk_work_session_user_contract")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}