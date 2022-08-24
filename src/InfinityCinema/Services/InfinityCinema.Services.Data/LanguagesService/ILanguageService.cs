namespace InfinityCinema.Services.Data.LanguagesService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;

    public interface ILanguageService
    {
        Task<Language> CreateAsync(string languageName);

        bool IsLanguageExist(string languageName);

        Language GetLanguageByName(string languageName);

        void DeleteLanguagesForParticularMovie(int movieId);
    }
}
