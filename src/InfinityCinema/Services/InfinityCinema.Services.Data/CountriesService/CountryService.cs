namespace InfinityCinema.Services.Data.CountriesService
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class CountryService : ICountryService
    {
        private const string TxtFilePath = @"C:\Users\User\Documents\GitHub\Infinity-Cinema-ASP.NET-Core-Project\src\InfinityCinema\Services\InfinityCinema.Services.Data\CountriesService\Files\Abbreviation-Country.txt";
        private readonly InfinityCinemaDbContext dbContext;

        public CountryService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create
        public async Task<T> CreateAsync<T>(string countryName)
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

            return this.GetViewModelById<T>(country.Id);
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
                .FirstOrDefault(c => c.Name.ToLower() == countryName.ToLower());

        public string GetCountryNameById(int id)
        {
            return this.dbContext.Countries.FirstOrDefault(c => c.Id == id).Name;
        }

        public T GetViewModelById<T>(int id)
            => this.dbContext
                .Countries
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();

        // Useful methods
        public bool CheckIfCountryExist(string countryName)
            => this.dbContext.Countries.Any(c => c.Name.Replace(" ", string.Empty).ToLower() == countryName.Replace(" ", string.Empty).ToLower());
    }
}
