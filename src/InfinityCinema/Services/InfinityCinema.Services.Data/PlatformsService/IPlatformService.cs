namespace InfinityCinema.Services.Data.PlatformsService
{
    using System.Threading.Tasks;

    public interface IPlatformService
    {
        Task<string> CreateAsync(CreatePlatformFormModel platformFormModel);
    }
}
