namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MovieCountryConfiguration : IEntityTypeConfiguration<MovieCountry>
    {
        public void Configure(EntityTypeBuilder<MovieCountry> movieCountryBuilder)
        {
            movieCountryBuilder
                .HasKey(m => new { m.MovieId, m.CountryId });

            movieCountryBuilder
                .HasOne(m => m.Movie)
                .WithMany(m => m.MovieCountries)
                .HasForeignKey(m => m.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            movieCountryBuilder
                .HasOne(m => m.Country)
                .WithMany(c => c.MovieCountries)
                .HasForeignKey(m => m.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
