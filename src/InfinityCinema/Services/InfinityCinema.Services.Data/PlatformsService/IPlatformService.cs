namespace InfinityCinema.Services.Data.PlatformsService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;

    public interface IPlatformService
    {
        Task<Platform> CreateAsync(PlatformFormModel platformFormModel);

        Platform GetPlatformByName(string platfrom);
    }
}
