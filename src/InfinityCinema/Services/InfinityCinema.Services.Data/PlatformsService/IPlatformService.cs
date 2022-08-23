﻿namespace InfinityCinema.Services.Data.PlatformsService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.PlatformsService.Models;

    public interface IPlatformService
    {
        Task<Platform> CreateAsync(PlatformFormModel platformFormModel);

        Platform GetPlatformByName(string platfrom);
    }
}
