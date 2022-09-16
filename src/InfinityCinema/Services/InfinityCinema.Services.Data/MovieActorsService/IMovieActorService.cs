namespace InfinityCinema.Services.Data.MovieActorsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMovieActorService
    {
        IEnumerable<T> GetActorsForGivenMovie<T>(int movieId);

        Task DeleteActorsForParticularMovie(int movieId);

        Task RemoveRelationBetweenMovieActorsAndActosTablesAsync(int actorId, int movieId);
    }
}
