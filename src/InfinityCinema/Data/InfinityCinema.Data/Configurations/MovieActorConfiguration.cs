namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> movieActorBuilder)
        {
            movieActorBuilder
                .HasKey(m => new { m.MovieId, m.ActorId });

            movieActorBuilder
                .HasOne(m => m.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(m => m.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            movieActorBuilder
                .HasOne(m => m.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(m => m.ActorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
