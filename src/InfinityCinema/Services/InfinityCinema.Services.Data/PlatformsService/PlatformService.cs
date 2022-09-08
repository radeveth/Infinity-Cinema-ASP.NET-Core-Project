namespace InfinityCinema.Services.Data.PlatformsService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.PlatformsService.Models;
    using InfinityCinema.Services.Mapping;

    public class PlatformService : IPlatformService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public PlatformService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create
        public async Task<T> CreateAsync<T>(PlatformFormModel platformFormModel)
        {
            Platform platform = new Platform()
            {
                Name = platformFormModel.Name,
                IconUrl = platformFormModel.IconUrl,
                PathUrl = platformFormModel.PathUrl,
            };

            await this.dbContext.AddAsync(platform);
            await this.dbContext.SaveChangesAsync();

            return this.GetViewModelById<T>(platform.Id);
        }

        // Read
        public IEnumerable<T> All<T>(string searchName = null)
        {
            IQueryable<Platform> platforms = this.dbContext.Platforms;

            if (searchName != null)
            {
                platforms = platforms.Where(p => p.Name.ToLower().Contains(searchName.ToLower()));
            }

            return platforms.To<T>();
        }

        public IEnumerable<T> GetPlatformsForGivenMovie<T>(int movieId)
        {
            IQueryable<Platform> platfromsForTargetMovie = this.dbContext.MoviePlatform
                .Where(m => m.MovieId == movieId)
                .Select(p => p.Platform);

            IEnumerable<T> platforms = platfromsForTargetMovie.To<T>();

            return platforms;
        }

        public T GetViewModelByName<T>(string platfrom)
            => this.dbContext
                .Platforms
                .Where(p => p.Name.ToLower() == platfrom.ToLower())
                .To<T>()
                .FirstOrDefault();

        public T GetViewModelById<T>(int id)
            => this.dbContext
                .Platforms
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefault();

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
