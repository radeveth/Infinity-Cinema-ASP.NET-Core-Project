namespace InfinityCinema.Services.Data.LanguagesService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.LanguagesService.Models;

    public class LanguageService : ILanguageService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public LanguageService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create
        public async Task<LanguageViewModel> CreateAsync(string languageName)
        {
            Language language = new Language()
            {
                Name = languageName,
            };

            await this.dbContext.Languages.AddAsync(language);
            await this.dbContext.SaveChangesAsync();

            return new LanguageViewModel()
            {
                Id = language.Id,
                Name = language.Name,
            };
        }

        // Read
        public LanguageViewModel GetLanguageByName(string languageName)
        {
            Language language = this.dbContext.Languages.FirstOrDefault(l => l.Name.ToLower() == languageName.ToLower());

            if (language == null)
            {
                return null;
            }

            return new LanguageViewModel()
            {
                Id = language.Id,
                Name = language.Name,
            };
        }

        public IEnumerable<string> GetLanguagesForParticularMovie(int movieId)
            => this.dbContext.MovieLanguages.Where(m => m.MovieId == movieId).Select(i => i.Language.Name);

        // Update

        // Delete
        public async Task DeleteLanguagesForParticularMovie(int movieId)
        {
            IQueryable<MovieLanguage> movieLanguages = this.dbContext.MovieLanguages.Where(m => m.MovieId == movieId);

            if (movieLanguages.Any())
            {
                this.dbContext.MovieLanguages.RemoveRange(movieLanguages);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public bool IsLanguageExist(string languageName)
            => this.dbContext.Languages.Any(l => l.Name.ToLower() == languageName.ToLower());
    }
}
