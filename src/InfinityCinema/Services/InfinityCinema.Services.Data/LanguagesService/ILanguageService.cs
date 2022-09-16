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

        // Update

        // Delete
        Task DeleteAsync(int id);

        bool IsLanguageExist(string languageName);
    }
}
