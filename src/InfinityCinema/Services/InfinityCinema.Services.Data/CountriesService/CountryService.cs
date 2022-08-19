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
            string abbreviation = string.Empty;

            string file = File.ReadAllText(@"C:\Users\User\Documents\GitHub\Infinity-Cinema-ASP.NET-Core-Project\src\InfinityCinema\Services\InfinityCinema.Services.Data\CountriesService\Abbreviation-Country.txt");

            string[] countries = file
                .Split("|", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            string targetCountry = null;
            foreach (string country in countries)
            {
                if (country.ToLower().Contains(countryName.ToLower()))
                {
                    targetCountry = country;
                }
            }

            int index = targetCountry.IndexOf(":");
            if (index == -1)
            {
                return " ";
            }

            abbreviation = targetCountry.Substring(0, index);

            return abbreviation;
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
