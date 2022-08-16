namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> directorBuilder)
        {
            directorBuilder
                .HasMany(d => d.DirectorMovies)
                .WithOne(d => d.Director)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
