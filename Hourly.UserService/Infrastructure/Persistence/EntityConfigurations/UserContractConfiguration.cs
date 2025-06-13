using Hourly.UserService.Domain.Entities;
using Hourly.UserService.Infrastructure.Persistence.Converters;
using Hourly.UserService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.Data.Persistence.EntityConfigurations
{
    public class UserContractConfiguration : IEntityTypeConfiguration<UserContract>
    {
        public void Configure(EntityTypeBuilder<UserContract> builder)
        {
            builder.ToTable("user_contract");

            builder.HasKey(uc => uc.Id);

            builder.Property(uc => uc.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(uc => uc.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(uc => uc.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(uc => uc.ContractType)
                .HasColumnName("contract_type")
                .IsRequired();

            builder.Property(uc => uc.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

            builder.Property(uc => uc.MinWeeklyHours)
                .HasColumnName("min_weekly_hours")
                .IsRequired();

            builder.Property(uc => uc.MaxWeeklyHours)
                .HasColumnName("max_weekly_hours")
                .IsRequired();

            builder.Property(uc => uc.GrossHourlyRate)
                .HasColumnName("gross_hourly_rate")
                .IsRequired(false);

            builder.Property(uc => uc.HolidayHoursPercentage)
                 .HasColumnName("holiday_hours_percentage")
                 .IsRequired(false);

            builder.Property(uc => uc.MonthlyPaidHolidayHours)
                .HasColumnName("monthly_paid_holiday_hours")
                .IsRequired();

            builder.Property(uc => uc.StartDate)
                .HasColumnName("start_date")
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(uc => uc.EndDate)
                .HasColumnName("end_date")
                .IsRequired(false)
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);

            builder.Property(uc => uc.ContractFilePath)
                .HasColumnName("contract_file_path")
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(uc => uc.Description)
                .HasColumnName("description")
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(uc => uc.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(uc => uc.UpdatedAt)
                .HasColumnName("updated_at")
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.Contracts)
                .HasForeignKey(uc => uc.UserId)
                .HasConstraintName("fk_user_contract_user_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(uc => uc.WorkSessions);

            builder.HasMany<WorkSessionReadModel>()
                .WithOne(ws => ws.UserContract)
                .HasForeignKey(ws => ws.UserContractId)
                .HasConstraintName("fk_work_session_user_contract_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}