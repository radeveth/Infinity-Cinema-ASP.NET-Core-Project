namespace InfinityCinema.Data.Configurations
{
    using InfinityCinema.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> actorBuilder)
        {
            actorBuilder
                .HasMany(a => a.MovieActors)
                .WithOne(m => m.Actor)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
