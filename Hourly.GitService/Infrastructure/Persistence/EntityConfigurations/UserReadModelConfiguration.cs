using Hourly.GitService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.GitService.Infrastructure.Persistence.EntityConfigurations
{
    public class UserReadModelConfiguration : IEntityTypeConfiguration<UserReadModel>
    {
        public void Configure(EntityTypeBuilder<UserReadModel> builder)
        {
            builder.ToTable("user_read_model");

            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("id");

            // Name (required)
            builder.Property(x => x.Name)
                   .HasColumnName("name")
                   .IsRequired()
                   .HasMaxLength(200);

            // GitEmail (optional)
            builder.Property(x => x.GitEmail)
                   .HasColumnName("git_email")
                   .HasMaxLength(200);

            // GitUsername (optional)
            builder.Property(x => x.GitUsername)
                   .HasColumnName("git_username")
                   .HasMaxLength(100);

            // GitAccessToken (optional, store with caution)
            builder.Property(x => x.GitAccessToken)
                   .HasColumnName("git_access_token")
                   .HasMaxLength(500); // or hashed/encrypted
        }
    }
}
