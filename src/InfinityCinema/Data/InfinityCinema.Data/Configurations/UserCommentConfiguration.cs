namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserCommentConfiguration : IEntityTypeConfiguration<UserComment>
    {
        public void Configure(EntityTypeBuilder<UserComment> userCommentBuilder)
        {
            userCommentBuilder
                .HasKey(u => new { u.UserId, u.CommentId });

            userCommentBuilder
                .HasOne(u => u.User)
                .WithMany(u => u.UserComments)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            userCommentBuilder
                .HasOne(u => u.Comment)
                .WithMany(c => c.UserComments)
                .HasForeignKey(u => u.CommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
