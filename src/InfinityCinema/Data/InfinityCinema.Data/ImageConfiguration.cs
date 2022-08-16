namespace InfinityCinema.Data
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> imageBuilder)
        {
            imageBuilder
                .HasOne(i => i.Movie)
                .WithMany(m => m.Images)
                .HasForeignKey(i => i.MovieId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
