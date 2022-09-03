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

        // Create
        public async Task<PlatformViewModel> CreateAsync(PlatformFormModel platformFormModel)
        {
            Platform platform = new Platform()
            {
                Name = platformFormModel.Name,
                IconUrl = platformFormModel.IconUrl,
                PathUrl = platformFormModel.PathUrl,
            };

            await this.dbContext.AddAsync(platform);
            await this.dbContext.SaveChangesAsync();

            return new PlatformViewModel()
            {
                Id = platform.Id,
                Name = platform.Name,
                Icon = platform.IconUrl,
                SiteUrl = platform.PathUrl,
            };
        }

        // Read
        public IEnumerable<PlatformViewModel> All(string searchName)
        {
            IEnumerable<PlatformViewModel> platforms = this.dbContext
                .Platforms
                .Select(p => new PlatformViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Icon = p.IconUrl,
                    SiteUrl = p.PathUrl,
                });

            if (searchName != null)
            {
                platforms = platforms.Where(p => p.Name.ToLower().Contains(searchName.ToLower()));
            }

            return platforms;
        }

        public IEnumerable<PlatformViewModel> GetPlatformsForGivenMovie(int movieId)
        {
            IQueryable<Platform> platfromsForTargetMovie = this.dbContext.MoviePlatform.Where(m => m.MovieId == movieId).Select(p => p.Platform);

            IEnumerable<PlatformViewModel> platforms = platfromsForTargetMovie.Select(p => new PlatformViewModel()
            {
                Name = p.Name,
                Icon = p.IconUrl,
                SiteUrl = p.PathUrl,
            });

            return platforms;
        }

        public PlatformViewModel GetPlatformByName(string platfrom)
        {
            Platform platform = this.dbContext.Platforms.FirstOrDefault(p => p.Name.ToLower() == platfrom.ToLower());

            if (platform != null)
            {
                return new PlatformViewModel()
                {
                    Id = platform.Id,
                    Name = platform.Name,
                    Icon = platform.IconUrl,
                    SiteUrl = platform.PathUrl,
                };
            }

            return null;
        }

        // Update

        // Delete
        public async Task DeletePlatformsForParticulatMovie(int movieId)
        {
            IQueryable<MoviePlatform> moviePlatforms = this.dbContext.MoviePlatform.Where(m => m.MovieId == movieId);

            this.dbContext.MoviePlatform.RemoveRange(moviePlatforms);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
