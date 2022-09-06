namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MovieUserCommentConfiguration : IEntityTypeConfiguration<MovieUserComment>
    {
        public void Configure(EntityTypeBuilder<MovieUserComment> movieUserCommentBuilder)
        {
            movieUserCommentBuilder
                .HasKey(m => new { m.MovieId, m.CommentId });

            movieUserCommentBuilder
                .HasOne(m => m.Movie)
                .WithMany(m => m.MovieUserComments)
                .HasForeignKey(m => m.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            movieUserCommentBuilder
                .HasOne(m => m.MovieComment)
                .WithMany(m => m.MovieUserComments)
                .HasForeignKey(m => m.CommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
