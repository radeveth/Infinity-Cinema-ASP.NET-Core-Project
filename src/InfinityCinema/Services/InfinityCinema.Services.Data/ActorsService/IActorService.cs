namespace InfinityCinema.Services.Data.ActorsService
{
    using InfinityCinema.Data.Models;
    using System.Threading.Tasks;

    public interface IActorService
    {
        Task<Actor> CreateAsync(ActorFormModel actorFormModel);
    }
}
