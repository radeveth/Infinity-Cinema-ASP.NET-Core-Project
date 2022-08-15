namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> movieGenreBuilder)
        {
            movieGenreBuilder
                .HasKey(m => new { m.MovieId, m.GenreId });

            movieGenreBuilder
                .HasOne(m => m.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(m => m.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            movieGenreBuilder
                .HasOne(m => m.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
