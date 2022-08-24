namespace InfinityCinema.Services.Data.CountriesService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;

    public interface ICountryService
    {
        Task<Country> CreateAsync(string countryName);

        bool CheckIfCountryExist(string countryName);

        Country GetCountryByName(string countryName);

        int GetCountryIdByGivenName(string givenName);

        string GenerateCountryAbbreviation(string countryName);

        string GetCountryNameById(int id);
    }
}
