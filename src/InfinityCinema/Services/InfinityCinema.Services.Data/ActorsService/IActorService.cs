namespace InfinityCinema.Services.Data.ActorsService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;

    public interface IActorService
    {
        Task<Actor> CreateAsync(ActorFormModel actorFormModel);

        Actor GetActorByNames(string firstName, string lastName);
    }
}
