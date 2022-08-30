namespace InfinityCinema.Services.Data.CountriesService
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.CountriesService.Models;

    public class CountryService : ICountryService
    {
        private const string TxtFilePath = @"C:\Users\User\Documents\GitHub\Infinity-Cinema-ASP.NET-Core-Project\src\InfinityCinema\Services\InfinityCinema.Services.Data\CountriesService\Files\Abbreviation-Country.txt";
        private readonly InfinityCinemaDbContext dbContext;

        public CountryService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create
        public async Task<CountryViewModel> CreateAsync(string countryName)
        {
            // Create country
            Country country = new Country()
            {
                Name = countryName,
                Abbreviation = this.GenerateCountryAbbreviation(countryName),
            };

            // Add country to database
            await this.dbContext.Countries.AddAsync(country);
            await this.dbContext.SaveChangesAsync();

            return new CountryViewModel()
            {
                Id = country.Id,
                Name = country.Name,
                Abbreviation = country.Abbreviation,
            };
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

        // Read
        public Country GetCountryByName(string countryName)
            => this.dbContext
                .Countries
                .FirstOrDefault(c => c.Name == countryName);

        public int GetCountryIdByGivenName(string givenName)
        {
            Country country = this.dbContext.Countries.FirstOrDefault(c => c.Name == givenName);

            if (country == null)
            {
                return 0;
            }

            return country.Id;
        }

        public string GetCountryNameById(int id)
        {
            Country country = this.dbContext.Countries.Find(id);

            return country.Name;
        }

        // Update
        public async Task<bool> EditCountryAsync(int countryId, string countryName)
        {
            try
            {
                Country country = this.dbContext.Countries.FirstOrDefault(c => c.Id == countryId);

                country.Name = countryName;
                country.Abbreviation = this.GenerateCountryAbbreviation(countryName);
            }
            catch (Exception)
            {
                throw new InvalidOperationException();
            }

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        // Delete

        // Useful methods
        public bool CheckIfCountryExist(string countryName)
            => this.dbContext.Countries.Any(c => c.Name.Replace(" ", string.Empty).ToLower() == countryName.Replace(" ", string.Empty).ToLower());

    }
}
