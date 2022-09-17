namespace InfinityCinema.Services.Data.MovieLanguagesService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;

    public class MovieLanguageService : IMovieLanguageService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public MovieLanguageService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<string> GetLanguagesForParticularMovie(int movieId)
            => this.dbContext.MovieLanguages.Where(m => m.MovieId == movieId).Select(i => i.Language.Name);

        public async Task DeleteLanguagesForParticularMovie(int movieId)
        {
            IQueryable<MovieLanguage> movieLanguages = this.dbContext.MovieLanguages.Where(m => m.MovieId == movieId);

            if (movieLanguages.Any())
            {
                foreach (var movieLanguage in movieLanguages)
                {
                    movieLanguage.IsDeleted = true;
                    movieLanguage.DeletedOn = DateTime.UtcNow;
                }

                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task MatchLanguagesWithMovie(int movieId, IEnumerable<int> languagesIds)
        {
            List<MovieLanguage> movieLanguages = new List<MovieLanguage>();

            foreach (int languageId in languagesIds.ToList())
            {
                movieLanguages.Add(new MovieLanguage() { MovieId = movieId, LanguageId = languageId });
            }

            await this.dbContext.MovieLanguages.AddRangeAsync(movieLanguages);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
