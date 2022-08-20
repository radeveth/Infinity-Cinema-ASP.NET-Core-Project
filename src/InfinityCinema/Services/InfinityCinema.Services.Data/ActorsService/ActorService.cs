namespace InfinityCinema.Services.Data.ActorsService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;

    public class ActorService : IActorService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public ActorService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Actor> CreateAsync(ActorFormModel actorFormModel)
        {
            Actor actor = new Actor()
            {
                FirstName = actorFormModel.FirstName,
                LastName = actorFormModel.LastName,
            };

            await this.dbContext.Actors.AddAsync(actor);
            await this.dbContext.SaveChangesAsync();

            return actor;
        }

        public Actor GetActorByNames(string firstName, string lastName)
            => this.dbContext.Actors
                .FirstOrDefault(a => $"{a.FirstName}{a.LastName}".ToLower() == $"{firstName}{lastName}".ToLower());
    }
}
