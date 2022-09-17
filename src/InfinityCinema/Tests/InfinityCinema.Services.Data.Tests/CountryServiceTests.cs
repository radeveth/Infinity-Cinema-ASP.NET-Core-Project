namespace InfinityCinema.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.CountriesService;
    using InfinityCinema.Services.Data.CountriesService.Models;
    using InfinityCinema.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CountryServiceTests
    {
        public CountryServiceTests()
        {
            this.InitializeMapper();
        }

        // Test #1
        [Fact]
        public async Task CreateAsyncShouldGenerateAbbreviationForCountryWithGivenNameAndAddNewCountryToDatabase()
        {
            // Arrange
            string countryName = "Bulgaria";
            string expectedAbbreviation = "BG";
            int expectedCountryCountInDatabase = 1;

            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(1));
            CountryService countryService = new CountryService(dbContext);

            // Act
            CountryViewModel countryViewModel = await countryService.CreateAsync<CountryViewModel>(countryName);

            // Assert
            Assert.Equal(expectedCountryCountInDatabase, dbContext.Countries.Count());
            Assert.Equal(countryName, countryViewModel.Name);
            Assert.Equal(expectedAbbreviation, countryViewModel.Abbreviation);
        }

        // Test #2
        [Fact]
        public async Task GetCountryByNameShouldReturnCountryIfCountryWithGivenNameExistingInDatabase()
        {
            // Arrange
            Country targetCountry = this.SeedCountries().First();

            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(2));
            List<Country> countries = this.SeedCountries();
            CountryService countryService = new CountryService(dbContext);

            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();

            // Act
            Country country = countryService.GetCountryByName(targetCountry.Name);

            // Assert
            Assert.Equal(targetCountry.Name, country.Name);
            Assert.Equal(targetCountry.Abbreviation, country.Abbreviation);
        }

        // Test #3
        [Fact]
        public async Task GetCountryNameByIdTest()
        {
            // Arrange
            Country targetCountry = this.SeedCountries().First();

            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(3));
            List<Country> countries = this.SeedCountries();
            CountryService countryService = new CountryService(dbContext);

            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();

            // Act
            string countryName = countryService.GetCountryNameById(targetCountry.Id);

            // Assert
            Assert.Equal(targetCountry.Name, countryName);
        }

        // Test #4
        [Fact]
        public async Task CheckIfCountryExistShouldReturnTrueIfCountryWithGivenNameExist()
        {
            // Arrange
            Country targetCountry = this.SeedCountries().First();

            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(4));
            List<Country> countries = this.SeedCountries();
            CountryService countryService = new CountryService(dbContext);

            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();

            // Act
            bool result = countryService.CheckIfCountryExist(targetCountry.Name);

            // Assert
            Assert.True(result);
        }

        // Test #5
        [Fact]
        public async Task CheckIfCountryExistShouldReturnFalseIfCountryWithGivenNameDoesNotExist()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(5));
            List<Country> countries = this.SeedCountries();
            CountryService countryService = new CountryService(dbContext);

            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();

            // Act
            bool result = countryService.CheckIfCountryExist("randomInvalidCountryName");

            // Assert
            Assert.False(result);
        }

        private List<Country> SeedCountries()
        {
            return new List<Country>()
            {
                new Country()
                {
                    Id = 1,
                    Name = "Bulgaria",
                    Abbreviation = "BG",
                },
                new Country()
                {
                    Id = 2,
                    Name = "USA",
                    Abbreviation = "USA",
                },
                new Country()
                {
                    Id = 3,
                    Name = "German",
                    Abbreviation = "DE",
                },
            };
        }

        private DbContextOptions<InfinityCinemaDbContext> PrepareOptionsForDbContext(int testCount)
        {
            return new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: $"CountryTestDb{testCount}").Options;
        }

        private void InitializeMapper() => AutoMapperConfig.RegisterMappings(typeof(CountryViewModel).Assembly);
    }
}
