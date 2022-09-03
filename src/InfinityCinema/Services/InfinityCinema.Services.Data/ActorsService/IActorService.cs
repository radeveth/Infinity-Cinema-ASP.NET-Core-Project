namespace InfinityCinema.Services.Data.ActorsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ActorsService.Models;

    public interface IActorService
    {
        // Create
        Task<ActorViewModel> CreateAsync(ActorFormModel actorFormModel);

        // Read
        IEnumerable<ActorViewModel> All(string searchName);

        ActorViewModel GetActorByNames(string fullName);

        IEnumerable<ActorViewModel> GetActorsForGivenMovie(int movieId);

        // Update

        // Delete
        Task DeleteActorsForParticularMovie(int movieId);
    }
}
