using Domain.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(p => p.Id);

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(c => c.MeetingId)
               .IsRequired();

            builder.Property(c => c.AuthorId)
               .IsRequired();

            //Relations
            builder.HasOne(c=> c.Meeting)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MeetingId)    

                //When meeting is deleted, delete comments
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Author)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.AuthorId)

                //When user is deleted leave his comments
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies) 
                .HasForeignKey(c => c.ParentCommentId)

                //When comment is deleted, delete replies
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
