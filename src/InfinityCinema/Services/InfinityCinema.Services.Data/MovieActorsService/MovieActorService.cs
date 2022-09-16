namespace InfinityCinema.Services.Data.MovieActorsService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class MovieActorService : IMovieActorService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public MovieActorService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<T> GetActorsForGivenMovie<T>(int movieId)
            => this.dbContext.MovieActors
                .Where(a => a.MovieId == movieId)
                .Select(m => m.Actor)
                .To<T>();

        public async Task DeleteActorsForParticularMovie(int movieId)
        {
            IQueryable<MovieActor> movieActors = this.dbContext.MovieActors.Where(m => m.MovieId == movieId);

            this.dbContext.MovieActors.RemoveRange(movieActors);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task RemoveRelationBetweenMovieActorsAndActosTablesAsync(int actorId, int movieId)
        {
            foreach (var movieActor in this.dbContext.MovieActors.Where(m => m.MovieId == movieId && m.ActorId == actorId).ToList())
            {
                movieActor.IsDeleted = true;
                movieActor.DeletedOn = DateTime.UtcNow;
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
