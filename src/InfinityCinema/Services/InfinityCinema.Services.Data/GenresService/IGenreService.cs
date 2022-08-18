namespace InfinityCinema.Services.Data.GenresService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;

    public interface IGenreService
    {
        Task<Genre> CreateAsync(GenreFormModel genreFormModel);

        IEnumerable<GenreFormModel> GetMovieGenres();

        bool IsGenresExists(ICollection<int> ids);
    }
}
