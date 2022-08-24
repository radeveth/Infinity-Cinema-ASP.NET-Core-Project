namespace InfinityCinema.Services.Data.LanguagesService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;

    public interface ILanguageService
    {
        Task<Language> CreateAsync(string languageName);

        bool IsLanguageExist(string languageName);

        Language GetLanguageByName(string languageName);

        Task DeleteLanguagesForParticularMovie(int movieId);

        IEnumerable<string> GetLanguagesForParticularMovie(int movieId);
    }
}
