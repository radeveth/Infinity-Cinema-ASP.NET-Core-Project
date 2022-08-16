namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> languageBuilder)
        {
            languageBuilder
                .HasMany(a => a.MovieLanguages)
                .WithOne(m => m.Language)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
