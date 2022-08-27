namespace InfinityCinema.Services.Data.CountriesService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.CountriesService.Models;

    public interface ICountryService
    {
        // Create
        Task<CountryViewModel> CreateAsync(string countryName);

        string GenerateCountryAbbreviation(string countryName);

        // Read
        Country GetCountryByName(string countryName);

        int GetCountryIdByGivenName(string givenName);

        string GetCountryNameById(int id);

        // Update
        // Delete

        bool CheckIfCountryExist(string countryName);
    }
}
