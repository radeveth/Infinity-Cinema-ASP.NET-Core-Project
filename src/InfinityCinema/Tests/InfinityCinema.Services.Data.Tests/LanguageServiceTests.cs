namespace InfinityCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.LanguagesService;
    using InfinityCinema.Services.Data.LanguagesService.Models;
    using InfinityCinema.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class LanguageServiceTests
    {
        public LanguageServiceTests()
        {
            this.InitializeMapper();
        }

        // Test #1
        [Fact]
        public async Task CreateAsyncShouldMapGivenLanguageNameToLanguageEntityAndAddNewLanguageToDatabse()
        {
            // Arrange
            int expectdLanguagesCountInDatabase = 1;
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(1));
            LanguageService languagesService = new LanguageService(dbContext);

            // Act
            await languagesService.CreateAsync<LanguageViewModel>("Bulgarian");

            // Assert
            Assert.Equal(expectdLanguagesCountInDatabase, dbContext.Languages.Count());
        }

        // Test #2
        [Fact]
        public async Task GetLanguageByNameTest()
        {
            // Arrange
            string targetLanguageName = "Bulgarian";
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(2));
            LanguageService languagesService = new LanguageService(dbContext);

            List<Language> languages = this.SeedLanguages();
            await dbContext.AddRangeAsync(languages);
            await dbContext.SaveChangesAsync();

            // Act
            LanguageViewModel languageViewModel = languagesService.GetLanguageByName<LanguageViewModel>(targetLanguageName);

            // Assert
            Assert.Equal(targetLanguageName, languageViewModel.Name);
        }

        // Test #3
        [Fact]
        public async Task DeleteAsyncTest()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(3));
            LanguageService languagesService = new LanguageService(dbContext);

            List<Language> languages = this.SeedLanguages();
            await dbContext.AddRangeAsync(languages);
            await dbContext.SaveChangesAsync();

            int expectedLanguagesCountAfterOperation = languages.Count - 1;

            // Act
            await languagesService.DeleteAsync(1);

            // Assert
            Assert.Equal(expectedLanguagesCountAfterOperation, dbContext.Languages.Count());

            // Arrange
            expectedLanguagesCountAfterOperation = expectedLanguagesCountAfterOperation - 1;

            // Act
            await languagesService.DeleteAsync(2);

            // Assert
            Assert.Equal(expectedLanguagesCountAfterOperation, dbContext.Languages.Count());
        }

        // Test #4
        [Fact]
        public async Task DeleteAsyncShouldThrowNullReferenceExceptionIfLanguageWithGiveIdDoesNotExist()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(4));
            LanguageService languagesService = new LanguageService(dbContext);

            List<Language> languages = this.SeedLanguages();
            await dbContext.AddRangeAsync(languages);
            await dbContext.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => languagesService.DeleteAsync(languages.Count + 100));
        }

        private List<Language> SeedLanguages()
        {
            return new List<Language>()
            {
                new Language()
                {
                    Name = "Bulgarian",
                },
                new Language()
                {
                    Name = "English",
                },
                new Language()
                {
                    Name = "German",
                },
            };
        }

        private DbContextOptions<InfinityCinemaDbContext> PrepareOptionsForDbContext(int testCount)
        {
            return new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: $"LanguageServiceTestDb{testCount}")
                .Options;
        }

        private void InitializeMapper() => AutoMapperConfig.RegisterMappings(typeof(LanguageViewModel).Assembly);
    }
}
