namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MovieCommentConfiguration : IEntityTypeConfiguration<MovieComment>
    {
        public void Configure(EntityTypeBuilder<MovieComment> movieCommentBuilder)
        {
            movieCommentBuilder
                .HasKey(m => new { m.MovieId, m.UserId });

            movieCommentBuilder
                .HasOne(m => m.Movie)
                .WithMany(m => m.Comments)
                .HasForeignKey(m => m.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            movieCommentBuilder
                .HasOne(m => m.User)
                .WithMany(u => u.CommentsCreated)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
