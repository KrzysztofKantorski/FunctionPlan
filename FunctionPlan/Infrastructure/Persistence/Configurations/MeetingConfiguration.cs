using Domain.Meetings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.ToTable("Meetings");
            builder.HasKey(p => p.Id);
            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(100);

            //Map Coordinates object to table columns
            builder.OwnsOne(m => m.Location, locationBuilder =>
            {
                locationBuilder.Property(c => c.Latitude)
                .HasColumnName("Latitude")
                .IsRequired();

                locationBuilder.Property(c => c.Longitude)
                .HasColumnName("Longitude")
                .IsRequired();
            }
            );
        }
    }
}
