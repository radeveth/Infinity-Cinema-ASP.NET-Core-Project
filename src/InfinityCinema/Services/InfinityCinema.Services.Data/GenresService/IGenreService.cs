namespace InfinityCinema.Services.Data.GenresService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.GenresService.Models;

    public interface IGenreService
    {
        // Create
        Task<GenreViewModel> CreateAsync(GenreFormModel genreFormModel);

        // Read
        IEnumerable<GenreViewModel> All();

        int GetGenreIdByGivenName(string genreName);

        IEnumerable<GenreFormModel> GetMovieGenres();

        IEnumerable<string> AllApplicationMovieGenres();

        IEnumerable<GenreViewModel> GetGenresForParticularMovie(int movieId);

        // Update

        // Delete
        Task DeleteGenresForParticularMovie(int movieId);

        bool IsGenresExists(IEnumerable<int> ids);
    }
}
