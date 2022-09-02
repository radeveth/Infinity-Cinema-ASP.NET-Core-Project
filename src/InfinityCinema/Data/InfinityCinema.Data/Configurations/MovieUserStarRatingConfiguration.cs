namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MovieUserStarRatingConfiguration : IEntityTypeConfiguration<MovieUserStarRating>
    {
        public void Configure(EntityTypeBuilder<MovieUserStarRating> starRatingBuilder)
        {
            starRatingBuilder
                .HasKey(s => new { s.UserId, s.MovieId });

            starRatingBuilder
                .HasOne(s => s.User)
                .WithMany(u => u.MovieUserStarRatings)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            starRatingBuilder
                .HasOne(s => s.Movie)
                .WithMany(m => m.MovieUserStarRatings)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
