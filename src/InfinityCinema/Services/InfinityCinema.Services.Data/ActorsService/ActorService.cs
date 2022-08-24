namespace InfinityCinema.Services.Data.ActorsService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ActorsService.Models;

    public class ActorService : IActorService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public ActorService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Actor> CreateAsync(ActorFormModel actorFormModel)
        {
            string[] actorNameParts = actorFormModel.FullName
                .Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            string firstName = actorNameParts[0];
            string lastName = actorNameParts[1];

            Actor actor = new Actor()
            {
                FirstName = firstName,
                LastName = lastName,
            };

            await this.dbContext.Actors.AddAsync(actor);
            await this.dbContext.SaveChangesAsync();

            return actor;
        }

        public Actor GetActorByNames(string fullName)
        {
            string[] actorNameParts = fullName
                .Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            string firstName = actorNameParts[0].ToLower();
            string lastName = actorNameParts[1].ToLower();

            return this.dbContext
                .Actors
                .AsQueryable()
                .FirstOrDefault(a => a.FirstName.ToLower() == firstName && a.LastName.ToLower() == lastName);
        }

        public IEnumerable<ActorViewModel> GetActorsForGivenMovie(int movieId)
        {
            IQueryable<Actor> actorsFromTargetMovie = this.dbContext.MovieActors.Where(a => a.MovieId == movieId).Select(m => m.Actor);

            IEnumerable<ActorViewModel> actors = actorsFromTargetMovie.Select(a => new ActorViewModel()
            {
                FullName = a.FirstName + " " + a.LastName,
                ImageUrl = a.ImageUrl,
            });

            return actors;
        }
    }
}
