namespace InfinityCinema.Services.Data.LanguagesService
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;

    public class LanguageService : ILanguageService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public LanguageService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Language> CreateAsync(string languageName)
        {
            Language language = new Language()
            {
                Name = languageName,
            };

            await this.dbContext.Languages.AddAsync(language);
            await this.dbContext.SaveChangesAsync();

            return language;
        }

        public bool IsLanguageExist(string languageName)
            => this.dbContext.Languages.Any(l => l.Name.ToLower() == languageName.ToLower());

        public Language GetLanguageByName(string languageName)
            => this.dbContext.Languages.FirstOrDefault(l => l.Name.ToLower() == languageName.ToLower());
    }
}
