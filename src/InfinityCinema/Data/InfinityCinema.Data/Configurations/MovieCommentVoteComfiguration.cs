namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MovieCommentVoteComfiguration : IEntityTypeConfiguration<MovieCommentVote>
    {
        public void Configure(EntityTypeBuilder<MovieCommentVote> movieCommentVoteBuilder)
        {
            movieCommentVoteBuilder.HasKey(m => new { m.UserId, m.MovieCommentId });

            movieCommentVoteBuilder
                .HasOne(m => m.MovieComment)
                .WithMany(m => m.MovieCommentVotes)
                .HasForeignKey(m => m.MovieCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            movieCommentVoteBuilder
                .HasOne(m => m.User)
                .WithMany(u => u.MovieCommentVotes)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
