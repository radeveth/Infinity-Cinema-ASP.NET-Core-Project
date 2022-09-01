namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserMoviesConfiguration : IEntityTypeConfiguration<ApplicationUserMovie>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserMovie> userMovieBuilder)
        {
            userMovieBuilder
                .HasKey(u => new { u.UserId, u.MovieId });

            userMovieBuilder
                .HasOne(u => u.User)
                .WithMany(u => u.ApplicationUserMovies)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            userMovieBuilder
                .HasOne(u => u.Movie)
                .WithMany(m => m.ApplicationUserMovies)
                .HasForeignKey(u => u.MovieId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
