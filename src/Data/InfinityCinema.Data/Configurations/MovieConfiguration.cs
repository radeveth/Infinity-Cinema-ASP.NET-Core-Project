namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> movieBuilder)
        {
            movieBuilder
                .HasMany(m => m.MovieGenres)
                .WithOne(m => m.Movie)
                .OnDelete(DeleteBehavior.Restrict);

            movieBuilder
                .HasMany(m => m.MovieActors)
                .WithOne(m => m.Movie)
                .OnDelete(DeleteBehavior.Restrict);

            movieBuilder
                .HasMany(m => m.MoviePlatforms)
                .WithOne(m => m.Movie)
                .OnDelete(DeleteBehavior.Restrict);

            movieBuilder
                .HasMany(m => m.MovieLanguages)
                .WithOne(m => m.Movie)
                .OnDelete(DeleteBehavior.Restrict);

            movieBuilder
                .HasMany(m => m.StarRatings)
                .WithOne(r => r.Movie)
                .OnDelete(DeleteBehavior.Restrict);

            movieBuilder
                .HasMany(m => m.Comments)
                .WithOne(r => r.Movie)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
