namespace InfinityCinema.Services.Data.ActorsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ActorsService.Models;

    public interface IActorService
    {
        // Create
        Task<T> CreateAsync<T>(ActorFormModel actorFormModel);

        Task CreateRowForMappingTableMovieActorsAsync(int movieId, int actorId);

        // Read
        IEnumerable<ActorViewModel> All(string searchName = null);

        ActorViewModel GetActorByNames(string fullName);

        T GetViewModelByIdAsync<T>(int id);

        // Update

        // Delete
        Task DeleteAsync(int id);
    }
}
