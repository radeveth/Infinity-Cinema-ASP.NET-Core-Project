namespace InfinityCinema.Services.Data.ActorsService
{
    using System.Threading.Tasks;

    public interface IActorService
    {
        Task<string> CreateAsync(ActorFormModel actorFprmModel);
    }
}
