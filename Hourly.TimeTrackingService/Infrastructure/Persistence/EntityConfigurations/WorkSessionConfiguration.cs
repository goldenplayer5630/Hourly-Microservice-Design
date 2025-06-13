using Hourly.TimeTrackingService.Domain.Entities;
using Hourly.TimeTrackingService.Infrastructure.Persistence.Converters;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence.EntityConfigurations
{
    public class WorkSessionConfiguration : IEntityTypeConfiguration<WorkSession>
    {
        public void Configure(EntityTypeBuilder<WorkSession> builder)
        {
            builder.ToTable("work_session");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.TaskDescription)
                .HasColumnName("task_description")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.StartTime)
                .HasColumnName("start_time")
                .IsRequired();

            builder.Property(x => x.EndTime)
                .HasColumnName("end_time")
                .IsRequired();

            builder.Property(x => x.BreakTime)
                .HasColumnName("break_time")
                .IsRequired();

            builder.Property(x => x.Factor)
                .HasColumnName("factor")
                .IsRequired();

            builder.Property(x => x.WBSO)
                .HasColumnName("wbso")
                .IsRequired();

            builder.Property(x => x.Locked)
                .HasColumnName("locked")
                .IsRequired();

            builder.Property(x => x.OtherRemarks)
                .HasColumnName("other_remarks")
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(x => x.TVTAccruedHours)
                .HasColumnName("tvt_accrued_hours")
                .IsRequired();

            builder.Property(x => x.TVTUsedHours)
                .HasColumnName("tvt_used_hours")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);

            builder.Property(x => x.UserContractId)
                .HasColumnName("user_contract_id");

            builder.HasOne(x => x.UserContract)
                .WithMany(uc => uc.WorkSessions)
                .HasForeignKey(x => x.UserContractId);

            builder.HasMany(ws => ws.GitCommits)
                .WithMany(gc => gc.WorkSessions)
                .UsingEntity<Dictionary<string, object>>(
                    "git_commit_work_sessions",

                    j => j.HasOne<GitCommitReadModel>()
                          .WithMany()
                          .HasForeignKey("git_commit_id")
                          .OnDelete(DeleteBehavior.Cascade),

                    j => j.HasOne<WorkSession>()
                          .WithMany()
                          .HasForeignKey("work_session_id")
                          .OnDelete(DeleteBehavior.Cascade),

                    j =>
                    {
                        j.HasKey("git_commit_id", "work_session_id");
                        j.ToTable("git_commit_work_sessions");
                    });

        }
    }
}
