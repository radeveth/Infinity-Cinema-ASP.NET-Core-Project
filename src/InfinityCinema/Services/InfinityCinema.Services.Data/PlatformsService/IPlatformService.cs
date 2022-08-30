namespace InfinityCinema.Services.Data.PlatformsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.PlatformsService.Models;

    public interface IPlatformService
    {
        // Create
        Task<PlatformViewModel> CreateAsync(PlatformFormModel platformFormModel);

        // Read
        PlatformViewModel GetPlatformByName(string platfrom);

        IEnumerable<PlatformViewModel> GetPlatformsForGivenMovie(int movieId);

        // Update

        // Delete
        Task DeletePlatformsForParticulatMovie(int movieId);
    }
}
