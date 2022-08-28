namespace InfinityCinema.Services.Data.GenresService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.GenresService.Models;

    public interface IGenreService
    {
        // Create
        Task<GenreViewModel> CreateAsync(GenreFormModel genreFormModel);

        // Read
        int GetGenreIdByGivenName(string genreName);

        IEnumerable<GenreFormModel> GetMovieGenres();

        IEnumerable<GenreViewModel> GetGenresForParticularMovie(int movieId);

        // Update

        // Delete
        Task DeleteGenresForParticularMovie(int movieId);

        bool IsGenresExists(IEnumerable<int> ids);
    }
}
