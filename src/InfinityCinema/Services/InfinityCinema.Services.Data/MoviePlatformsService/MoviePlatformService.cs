namespace InfinityCinema.Services.Data.MoviePlatformsService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class MoviePlatformService : IMoviePlatformService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public MoviePlatformService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateRowForMappingTableMoviePlatformsAsync(int movieId, int platformId)
        {
            MoviePlatform moviePlatform = new MoviePlatform() { MovieId = movieId, PlatformId = platformId };

            await this.dbContext.MoviePlatform.AddAsync(moviePlatform);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetPlatformsForGivenMovie<T>(int movieId)
        {
            IQueryable<Platform> platfromsForTargetMovie = this.dbContext.MoviePlatform
                .Where(m => m.MovieId == movieId)
                .Select(p => p.Platform);

            IEnumerable<T> platforms = platfromsForTargetMovie.To<T>();

            return platforms;
        }

        public async Task DeletePlatformsForParticulatMovie(int movieId)
        {
            IQueryable<MoviePlatform> moviePlatforms = this.dbContext.MoviePlatform.Where(m => m.MovieId == movieId);

            if (moviePlatforms.Any())
            {
                foreach (var moviePlatform in moviePlatforms)
                {
                    moviePlatform.IsDeleted = true;
                    moviePlatform.DeletedOn = DateTime.UtcNow;
                }

                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveRelationBetweenMoviePlatformsAndPlatformsTablesAsync(int platformId, int movieId)
        {
            foreach (var moviePlatform in this.dbContext.MoviePlatform.Where(m => m.PlatformId == platformId && m.MovieId == movieId).ToList())
            {
                moviePlatform.IsDeleted = true;
                moviePlatform.DeletedOn = DateTime.UtcNow;
            }

            await this.dbContext.SaveChangesAsync();
        }

        public async Task MatchPlatformsWithMovie(int movieId, ICollection<int> platformsIds)
        {
            ICollection<MoviePlatform> moviePlatforms = new HashSet<MoviePlatform>();

            foreach (int platformId in platformsIds)
            {
                moviePlatforms.Add(new MoviePlatform() { MovieId = movieId, PlatformId = platformId });
            }

            await this.dbContext.AddRangeAsync(moviePlatforms);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
