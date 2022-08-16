namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MovieLanguageConfiguration : IEntityTypeConfiguration<MovieLanguage>
    {
        public void Configure(EntityTypeBuilder<MovieLanguage> movieLanguageBuilder)
        {
            movieLanguageBuilder
                .HasKey(m => new { m.MovieId, m.LanguageId });

            movieLanguageBuilder
                .HasOne(m => m.Movie)
                .WithMany(m => m.MovieLanguages)
                .HasForeignKey(m => m.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            movieLanguageBuilder
                .HasOne(m => m.Language)
                .WithMany(l => l.MovieLanguages)
                .HasForeignKey(m => m.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
