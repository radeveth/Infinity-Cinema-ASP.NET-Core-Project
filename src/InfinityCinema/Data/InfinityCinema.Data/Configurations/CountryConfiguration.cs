namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> countryBuilder)
        {
            countryBuilder
                .HasMany(c => c.Movies)
                .WithOne(m => m.Country)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
