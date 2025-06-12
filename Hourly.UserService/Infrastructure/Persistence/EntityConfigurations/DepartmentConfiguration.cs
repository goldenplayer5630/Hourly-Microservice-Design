using Hourly.UserService.Infrastructure.Persistence.Converters;
using Hourly.UserService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hourly.UserService.Infrastructure.Persistence.ReadModels;

namespace Hourly.Data.Persistence.EntityConfigurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("department");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name");

            builder.HasMany(x => x.Users)
                .WithOne(u => u.Department)
                .HasForeignKey(u => u.DepartmentId);

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
