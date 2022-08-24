namespace InfinityCinema.Services.Data.PlatformsService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.PlatformsService.Models;

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

        public IEnumerable<PlatformViewModel> GetPlatformsForGivenMovie(int movieId)
        {
            IQueryable<Platform> platfromsForTargetMovie = this.dbContext.MoviePlatform.Where(m => m.MovieId == movieId).Select(p => p.Platform);

            IEnumerable<PlatformViewModel> platforms = platfromsForTargetMovie.Select(p => new PlatformViewModel()
            {
                Name = p.Name,
                Icon = p.IconUrl,
                PathUrl = p.PathUrl,
            });

            return platforms;
        }

        public Platform GetPlatformByName(string platfrom)
            => this.dbContext.Platforms.FirstOrDefault(p => p.Name.ToLower() == platfrom.ToLower());
    }
}
