namespace InfinityCinema.Services.Data.CountriesService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;

    public interface ICountryService
    {
        // Create
        Task<T> CreateAsync<T>(string countryName);

        string GenerateCountryAbbreviation(string countryName);

        // Read
        Country GetCountryByName(string countryName);

        string GetCountryNameById(int id);

        // Useful methods
        bool CheckIfCountryExist(string countryName);
    }
}
