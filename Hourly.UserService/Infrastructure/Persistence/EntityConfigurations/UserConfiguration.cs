﻿using Hourly.UserService.Domain.Entities;
using Hourly.UserService.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.UserService.Infrastructure.Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("email");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name");

            builder.Property(x => x.GitEmail)
                .IsRequired(false)
                .HasColumnName("git_email");

            builder.Property(x => x.GitUsername)
                .IsRequired(false)
                .HasColumnName("git_username");

            builder.Property(x => x.GitAccessToken)
                .IsRequired(false)
                .HasColumnName("git_access_token");

            builder.Property(x => x.TVTHourBalance)
                .IsRequired()
                .HasColumnName("tvt_hour_balance");

            builder.Property(x => x.Role)
                .HasColumnName("role");

            builder.Property(x => x.DepartmentId)
                .HasColumnName("department_id");

            builder.HasOne(x => x.Department)
                .WithMany(d => d.Users)
                .IsRequired(false)
                .HasForeignKey(x => x.DepartmentId);

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