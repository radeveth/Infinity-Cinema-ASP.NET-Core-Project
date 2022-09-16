namespace InfinityCinema.Services.Data.GenresService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.GenresService.Models;

    public interface IGenreService
    {
        // Create
        Task<T> CreateAsync<T>(GenreFormModel genreFormModel);

        // Read
        IEnumerable<T> All<T>(string seacrhName);

        int GetGenreIdByGivenName(string genreName);

        IEnumerable<GenreFormModel> GetMovieGenres();

        IEnumerable<string> AllApplicationMovieGenres();

        // Update

        // Delete
        Task DeleteAsync(int id);

        bool IsGenresExists(IEnumerable<int> ids);
    }
}
