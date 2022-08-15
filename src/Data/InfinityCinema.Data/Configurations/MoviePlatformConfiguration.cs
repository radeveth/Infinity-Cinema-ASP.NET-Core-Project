namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MoviePlatformConfiguration : IEntityTypeConfiguration<MoviePlatform>
    {
        public void Configure(EntityTypeBuilder<MoviePlatform> moviePlatformBuilder)
        {
            moviePlatformBuilder
                .HasKey(m => new { m.MovieId, m.PlatformId });

            moviePlatformBuilder
                .HasOne(m => m.Movie)
                .WithMany(m => m.MoviePlatforms)
                .HasForeignKey(m => m.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            moviePlatformBuilder
                .HasOne(m => m.Platform)
                .WithMany(p => p.MoviePlatforms)
                .HasForeignKey(m => m.PlatformId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
