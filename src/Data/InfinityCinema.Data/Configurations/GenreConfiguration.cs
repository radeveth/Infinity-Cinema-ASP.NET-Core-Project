namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> genreBuilder)
        {
            genreBuilder
                .HasMany(a => a.MovieGenres)
                .WithOne(m => m.Genre)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
