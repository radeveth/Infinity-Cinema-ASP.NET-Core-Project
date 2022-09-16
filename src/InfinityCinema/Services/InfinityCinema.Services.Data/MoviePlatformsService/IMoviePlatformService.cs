namespace InfinityCinema.Services.Data.MoviePlatformsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMoviePlatformService
    {
        public Task CreateRowForMappingTableMoviePlatformsAsync(int movieId, int platformId);

        IEnumerable<T> GetPlatformsForGivenMovie<T>(int movieId);

        Task DeletePlatformsForParticulatMovie(int movieId);

        Task RemoveRelationBetweenMoviePlatformsAndPlatformsTablesAsync(int platformId, int movieId);
    }
}
