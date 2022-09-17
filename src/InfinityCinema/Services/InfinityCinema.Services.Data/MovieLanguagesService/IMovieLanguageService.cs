namespace InfinityCinema.Services.Data.MovieLanguagesService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMovieLanguageService
    {
        IEnumerable<string> GetLanguagesForParticularMovie(int movieId);

        Task DeleteLanguagesForParticularMovie(int movieId);

        Task MatchLanguagesWithMovie(int movieId, IEnumerable<int> languagesIds);
    }
}
