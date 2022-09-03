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
                .HasMany(m => m.Comments)
                .WithOne(r => r.Movie)
                .OnDelete(DeleteBehavior.Restrict);

            movieBuilder
                .HasMany(m => m.Images)
                .WithOne(i => i.Movie)
                .OnDelete(DeleteBehavior.Restrict);

            movieBuilder
                .HasOne(m => m.Director)
                .WithMany(d => d.Movies)
                .HasForeignKey(m => m.DirectorId)
                .OnDelete(DeleteBehavior.Restrict);

            movieBuilder
                .HasOne(m => m.Country)
                .WithMany(c => c.Movies)
                .HasForeignKey(m => m.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            movieBuilder
                .HasOne(m => m.User)
                .WithMany(u => u.MoviesCreated)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
