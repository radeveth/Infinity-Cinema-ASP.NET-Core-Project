namespace InfinityCinema.Services.Data.GenresService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenreService
    {
        Task<string> CreateAsync(GenreFormModel genreFormModel);

        IEnumerable<GenreFormModel> GetMovieGenres();
    }
}
