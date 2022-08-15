namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> platformBuilder)
        {
            platformBuilder
                .HasMany(p => p.MoviePlatforms)
                .WithOne(m => m.Platform)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
