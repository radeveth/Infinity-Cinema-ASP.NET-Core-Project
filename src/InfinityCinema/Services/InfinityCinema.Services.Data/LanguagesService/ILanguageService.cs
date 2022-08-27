namespace InfinityCinema.Services.Data.LanguagesService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.LanguagesService.Models;

    public interface ILanguageService
    {
        // Create
        Task<LanguageViewModel> CreateAsync(string languageName);

        // Read
        LanguageViewModel GetLanguageByName(string languageName);

        IEnumerable<string> GetLanguagesForParticularMovie(int movieId);

        // Update

        // Delete
        Task DeleteLanguagesForParticularMovie(int movieId);

        bool IsLanguageExist(string languageName);
    }
}
