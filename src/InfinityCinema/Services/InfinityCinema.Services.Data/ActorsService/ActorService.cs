namespace InfinityCinema.Services.Data.ActorsService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ActorsService.Models;
    using InfinityCinema.Services.Mapping;

    public class ActorService : IActorService
    {
        private const string SPLITING_ACTOR_FULL_NAME = " ";

        private readonly InfinityCinemaDbContext dbContext;

        public ActorService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create
        public async Task<T> CreateAsync<T>(ActorFormModel actorFormModel)
        {
            // Get (split) first and last name of actor
            string[] actorNameParts = this.SplitActorFullName(actorFormModel.FullName);
            string firstName = actorNameParts[0];
            string lastName = actorNameParts[1];

            // Create actor
            Actor actor = new Actor()
            {
                FirstName = firstName,
                LastName = lastName,
                ImageUrl = actorFormModel.ImageUrl,
                InformationLink = actorFormModel.InformationLink,
            };

            // Add actor to database
            await this.dbContext.Actors.AddAsync(actor);
            await this.dbContext.SaveChangesAsync();

            return this.GetViewModelByIdAsync<T>(actor.Id);
        }

        // Read
        public IEnumerable<ActorViewModel> All(string searchName = null)
        {
            IEnumerable<ActorViewModel> actors = this.dbContext
                    .Actors
                     .To<ActorViewModel>();

            if (searchName != null)
            {
                actors = actors.Where(a => a.FullName.ToLower().Contains(searchName.ToLower()));
            }

            return actors;
        }

        public ActorViewModel GetActorByNames(string fullName)
        {
            // Get (split) first and last name of actor
            string[] actorNameParts = this.SplitActorFullName(fullName);
            string firstName = actorNameParts[0].ToLower();
            string lastName = actorNameParts[1].ToLower();

            // Find the actor
            Actor actor = this.dbContext
                .Actors
                .AsQueryable()
                .FirstOrDefault(a => a.FirstName.ToLower() == firstName && a.LastName.ToLower() == lastName);

            // Return the actor view model
            if (actor != null)
            {
                return this.GetViewModelByIdAsync<ActorViewModel>(actor.Id);
            }

            return null;
        }

        public IEnumerable<T> GetActorsForGivenMovie<T>(int movieId)
            => this.dbContext.MovieActors
                .Where(a => a.MovieId == movieId)
                .Select(m => m.Actor)
                .To<T>();

        public T GetViewModelByIdAsync<T>(int id)
            => this.dbContext
                .Actors
                .Where(a => a.Id == id)
                .To<T>()
                .FirstOrDefault();

        // Update

        // Delete
        public async Task DeleteActorsForParticularMovie(int movieId)
        {
            IQueryable<MovieActor> movieActors = this.dbContext.MovieActors.Where(m => m.MovieId == movieId);

            this.dbContext.MovieActors.RemoveRange(movieActors);
            await this.dbContext.SaveChangesAsync();
        }

        // Useful private methods
        private string[] SplitActorFullName(string fullName)
            => fullName
                .Split(SPLITING_ACTOR_FULL_NAME, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
    }
}
