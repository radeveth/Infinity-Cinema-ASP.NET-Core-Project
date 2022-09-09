namespace InfinityCinema.Services.Data.LanguagesService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILanguageService
    {
        // Create
        Task<T> CreateAsync<T>(string languageName);

        // Read
        T GetLanguageByName<T>(string languageName);

        T GetViewModelById<T>(int id);

        IEnumerable<string> GetLanguagesForParticularMovie(int movieId);

        // Update

        // Delete
        Task DeleteLanguagesForParticularMovie(int movieId);

        bool IsLanguageExist(string languageName);
    }
}
