using Domain.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
    {
        public void Configure(EntityTypeBuilder<MediaFile> builder)
        {
            builder.ToTable("MediaFiles");

            builder.HasKey(p => p.Id);

            builder.Property(c => c.FileName)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(c => c.MeetingId)
               .IsRequired();

            builder.Property(c => c.UploaderId)
               .IsRequired();

            //Relation with meeting
            builder.HasOne(c => c.Meeting)
                
                .WithMany(m => m.MediaFiles)
                .HasForeignKey(c => c.MeetingId)

                //If meeting is deleted, delete its media
                .OnDelete(DeleteBehavior.Cascade);


            //Relation with user
            builder.HasOne(c => c.Uploader)
                .WithMany(m => m.MediaFiles)
                .HasForeignKey(c => c.UploaderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
