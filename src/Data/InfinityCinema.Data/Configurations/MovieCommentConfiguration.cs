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
                .HasOne(m => m.Movie)
                .WithMany(m => m.Comments)
                .HasForeignKey(m => m.MovieId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
