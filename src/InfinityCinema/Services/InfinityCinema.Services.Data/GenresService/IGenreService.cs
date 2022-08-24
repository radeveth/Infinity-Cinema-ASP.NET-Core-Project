namespace InfinityCinema.Services.Data.GenresService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.GenresService.Models;

    public interface IGenreService
    {
        Task<Genre> CreateAsync(GenreFormModel genreFormModel);

        IEnumerable<GenreFormModel> GetMovieGenres();

        bool IsGenresExists(IEnumerable<int> ids);

        int GetGenreIdByGivenName(string genreName);

        Task DeleteGenresForParticularMovie(int movieId);
    }
}
