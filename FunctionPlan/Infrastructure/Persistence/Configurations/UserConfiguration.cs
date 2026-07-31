using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(m => m.Username)
               .IsRequired()
               .HasMaxLength(30);

            builder.Property(m => m.Email)
               .IsRequired();

            builder.Property(m => m.Role)
              .IsRequired()
              .HasConversion<string>();
        }
    }
}
