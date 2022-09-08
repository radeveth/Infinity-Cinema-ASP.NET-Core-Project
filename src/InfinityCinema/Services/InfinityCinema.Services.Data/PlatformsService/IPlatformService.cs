namespace InfinityCinema.Services.Data.PlatformsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.PlatformsService.Models;

    public interface IPlatformService
    {
        // Create
        Task<T> CreateAsync<T>(PlatformFormModel platformFormModel);

        // Read
        IEnumerable<T> All<T>(string searchName = null);

        T GetViewModelByName<T>(string platfrom);

        IEnumerable<T> GetPlatformsForGivenMovie<T>(int movieId);

        T GetViewModelById<T>(int id);

        // Update

        // Delete
        Task DeletePlatformsForParticulatMovie(int movieId);
    }
}
