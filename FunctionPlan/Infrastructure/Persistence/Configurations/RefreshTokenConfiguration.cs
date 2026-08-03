using Domain.RefreshTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.HasKey(p => p.Id);
            builder.Property(m => m.UserId)
              .IsRequired();
            builder.Property(m => m.Token)
                .IsRequired()
                .HasMaxLength(200);

            //Relation with user
            builder.HasOne(rt => rt.User)
                .WithMany() 
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
