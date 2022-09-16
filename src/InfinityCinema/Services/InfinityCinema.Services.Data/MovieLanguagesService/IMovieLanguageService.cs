namespace InfinityCinema.Services.Data.MovieLanguagesService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMovieLanguageService
    {
        IEnumerable<string> GetLanguagesForParticularMovie(int movieId);

        public Task DeleteLanguagesForParticularMovie(int movieId);
    }
}
