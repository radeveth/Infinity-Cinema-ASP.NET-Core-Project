namespace InfinityCinema.Services.Data.CountriesService
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;

    public class CountryService : ICountryService
    {
        private const string TxtFilePath = @"C:\Users\User\Documents\GitHub\Infinity-Cinema-ASP.NET-Core-Project\src\InfinityCinema\Services\InfinityCinema.Services.Data\CountriesService\Abbreviation-Country.txt";
        private readonly InfinityCinemaDbContext dbContext;

        public CountryService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Add Country to Database
        public async Task<Country> CreateAsync(string countryName)
        {
            Country country = new Country()
            {
                Name = countryName,
                Abbreviation = this.GenerateCountryAbbreviation(countryName),
            };

            await this.dbContext.Countries.AddAsync(country);
            await this.dbContext.SaveChangesAsync();

            return country;
        }

        public string GenerateCountryAbbreviation(string countryName)
        {
            countryName = countryName.Replace(" ", string.Empty).ToLower();

            string file = File.ReadAllText(TxtFilePath);

            string[] countriesAbbreviation = file
                .Split("|", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            string targetCountryAbbreviation = string.Empty;
            foreach (var country in countriesAbbreviation)
            {
                string[] countryAbbreviationParts = country
                    .Split("-", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
                string currCountryAbbreviation = countryAbbreviationParts[0];
                string currCountryName = countryAbbreviationParts[1].Replace(" ", string.Empty).ToLower();

                if (currCountryName == countryName)
                {
                    targetCountryAbbreviation = currCountryAbbreviation;
                    break;
                }
            }

            return targetCountryAbbreviation;
        }

        public Country GetCountryByName(string countryName)
            => this.dbContext
                .Countries
                .FirstOrDefault(c => c.Name == countryName);

        public int GetCountryIdByGivenName(string givenName)
        {
            Country country = this.dbContext.Countries.FirstOrDefault(c => c.Name == givenName);

            return country.Id;
        }

        public bool CheckIfCountryExist(string countryName)
            => this.dbContext.Countries.Any(c => c.Name.Replace(" ", string.Empty).ToLower() == countryName.Replace(" ", string.Empty).ToLower());
    }
}
