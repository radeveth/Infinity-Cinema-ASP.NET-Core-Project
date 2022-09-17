namespace InfinityCinema.Services.Data.LanguagesService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AngleSharp.Html;
    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class LanguageService : ILanguageService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public LanguageService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create
        public async Task<T> CreateAsync<T>(string languageName)
        {
            Language language = new Language()
            {
                Name = languageName,
            };

            await this.dbContext.Languages.AddAsync(language);
            await this.dbContext.SaveChangesAsync();

            return this.GetViewModelById<T>(language.Id);
        }

        // Read
        public T GetLanguageByName<T>(string languageName)
            => this.dbContext
                .Languages
                .Where(l => l.Name.ToLower() == languageName.ToLower())
                .To<T>()
                .FirstOrDefault();

        public T GetViewModelById<T>(int id)
            => this.dbContext
                .Languages
                .Where(i => i.Id == id)
                .To<T>()
                .FirstOrDefault();

        // Update

        // Delete
        public async Task DeleteAsync(int id)
        {
            Language language = await this.dbContext.Languages.FindAsync(id);

            if (language == null)
            {
                throw new NullReferenceException();
            }

            language.IsDeleted = true;
            language.DeletedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public bool IsLanguageExist(string languageName)
            => this.dbContext.Languages.Any(l => l.Name.ToLower() == languageName.ToLower());
    }
}
