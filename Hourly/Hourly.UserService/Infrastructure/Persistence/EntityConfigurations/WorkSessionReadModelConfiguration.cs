using Hourly.UserService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.UserService.Infrastructure.Persistence.EntityConfigurations
{
    public class WorkSessionReadModelConfiguration : IEntityTypeConfiguration<WorkSessionReadModel>
    {
        public void Configure(EntityTypeBuilder<WorkSessionReadModel> builder)
        {
            builder.ToTable("work_session_read_model");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.UserContractId)
                .HasColumnName("user_contract_id")
                .IsRequired();

            builder.HasOne(x => x.UserContract)
                .WithMany(uc => uc.WorkSessions)
                .HasForeignKey(x => x.UserContractId)
                .HasConstraintName("fk_work_session_read_model_user_contract");
        }
    }
}
