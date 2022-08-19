namespace InfinityCinema.Services.Data.PlatformsService
{
    using System;
    using System.Threading.Tasks;
    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;

    public class PlatformService : IPlatformService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public PlatformService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Platform> CreateAsync(PlatformFormModel platformFormModel)
        {
            Platform platform = new Platform()
            {
                Name = platformFormModel.Name,
                PathUrl = platformFormModel.PathUrl,
                IconUrl = platformFormModel.IconUrl,
            };

            await this.dbContext.AddAsync(platform);
            await this.dbContext.SaveChangesAsync();

            return platform;
        }
    }
}
