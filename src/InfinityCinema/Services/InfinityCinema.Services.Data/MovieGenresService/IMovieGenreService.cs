namespace InfinityCinema.Services.Data.MovieGenresService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMovieGenreService
    {
        IEnumerable<T> GetGenresForParticularMovie<T>(int movieId);

        Task DeleteGenresForParticularMovie(int movieId);

        Task MatchGenresWithMovie(int movieId, IEnumerable<int> genresIds);
    }
}
