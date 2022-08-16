namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DirectorMovieConfiguration : IEntityTypeConfiguration<DirectorMovie>
    {
        public void Configure(EntityTypeBuilder<DirectorMovie> directorMovieBuilder)
        {
            directorMovieBuilder
                .HasKey(d => new { d.DirectorId, d.MovieId });

            directorMovieBuilder
                .HasOne(d => d.Movie)
                .WithMany(m => m.DirectorMovies)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            directorMovieBuilder
                .HasOne(d => d.Director)
                .WithMany(d => d.DirectorMovies)
                .HasForeignKey(d => d.DirectorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
