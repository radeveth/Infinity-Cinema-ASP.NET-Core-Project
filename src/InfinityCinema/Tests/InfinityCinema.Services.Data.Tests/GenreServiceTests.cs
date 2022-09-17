namespace InfinityCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.GenresService.Models;
    using InfinityCinema.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class GenreServiceTests
    {
        public GenreServiceTests()
        {
            this.InitializeMapper();
        }

        // Test #1
        [Fact]
        public async Task CreateAsyncShouldMapGenreFormModelToGenreEntityAndAddNewGenreToDatabase()
        {
            // Arrange
            int expectedCountOfGenresInDatabase = 1;
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(1));
            GenreService genreService = new GenreService(dbContext);

            // Act
            await genreService.CreateAsync<GenreViewModel>(new GenreFormModel()
            {
                Name = "Action",
                Description = "Movies in the action genre are fast-paced and include a lot of action like fight scenes, chase scenes, and slow-motion shots.",
                ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000023/16084899-clap-film-of-cinema-action-genre-clapperboard-text-illustration-.jpg?fj=1",
            });

            // Assert
            Assert.Equal(expectedCountOfGenresInDatabase, dbContext.Genres.Count());
        }

        // Test #2
        [Fact]
        public async Task AllTest()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(2));
            GenreService genreService = new GenreService(dbContext);

            List<Genre> genres = this.SeedGenres();
            await dbContext.Genres.AddRangeAsync(genres);
            await dbContext.SaveChangesAsync();

            int expectedCountAfterOperation = dbContext.Genres.Count();

            // Act
            IEnumerable<GenreViewModel> genreViewModels = genreService.All<GenreViewModel>();

            // Assert
            Assert.Equal(expectedCountAfterOperation, genreViewModels.Count());
        }

        // Test #3
        [Fact]
        public async Task AllShouldReturnGenreaIfHaveParamatersGiven()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(3));
            GenreService genreService = new GenreService(dbContext);

            List<Genre> genres = this.SeedGenres();
            await dbContext.Genres.AddRangeAsync(genres);
            await dbContext.SaveChangesAsync();

            int expectedCountAfterOperation = 1;

            // Act
            IEnumerable<GenreViewModel> genreViewModels = genreService.All<GenreViewModel>("action1");

            // Assert
            Assert.Equal(expectedCountAfterOperation, genreViewModels.Count());

            // ---
            // Arrange
            expectedCountAfterOperation = 3;

            // Act
            genreViewModels = genreService.All<GenreViewModel>("action");

            // Assert
            Assert.Equal(expectedCountAfterOperation, genreViewModels.Count());
        }

        // Test #4
        [Fact]
        public async Task DeleteAsyncTest()
        {
            // Arrange
            int expectedResult = 2;
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(4));
            GenreService genreService = new GenreService(dbContext);

            List<Genre> genres = this.SeedGenres();
            await dbContext.Genres.AddRangeAsync(genres);
            await dbContext.SaveChangesAsync();

            // Act
            await genreService.DeleteAsync(1);

            // Assert
            Assert.Equal(expectedResult, dbContext.Genres.Count());
        }

        // Test #5
        [Fact]
        public async Task DeleteAsyncShouldThrowNullReferenceExceptionIfGenreWithGivenIdDoesNotExist()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(5));
            GenreService genreService = new GenreService(dbContext);

            List<Genre> genres = this.SeedGenres();
            await dbContext.Genres.AddRangeAsync(genres);
            await dbContext.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await genreService.DeleteAsync(genres.Count + 100));
        }

        // Test #6
        [Fact]
        public async Task IsGenresExistsShouldReturnTrue()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(6));
            GenreService genreService = new GenreService(dbContext);

            List<Genre> genres = this.SeedGenres();
            await dbContext.Genres.AddRangeAsync(genres);
            await dbContext.SaveChangesAsync();

            // Act
            bool result = genreService.IsGenresExists(new int[] { 1, 2, 3 });

            // Assert
            Assert.True(result);
        }

        // Test #7
        [Fact]
        public async Task IsGenresExistsShouldReturnFalse()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(7));
            GenreService genreService = new GenreService(dbContext);

            List<Genre> genres = this.SeedGenres();
            await dbContext.Genres.AddRangeAsync(genres);
            await dbContext.SaveChangesAsync();

            // Act
            bool result = genreService.IsGenresExists(new int[] { 1, 2, 4, 3 });

            // Assert
            Assert.False(result);
        }

        private List<Genre> SeedGenres()
        {
            return new List<Genre>()
            {
                new Genre()
                {
                    Name = "Action1",
                    Description = "Movies in the action genre are fast-paced and include a lot of action like fight scenes, chase scenes, and slow-motion shots.1",
                    ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000023/16084899-clap-film-of-cinema-action-genre-clapperboard-text-illustration-.jpg?fj=11",
                },
                new Genre()
                {
                    Name = "Action2",
                    Description = "Movies in the action genre are fast-paced and include a lot of action like fight scenes, chase scenes, and slow-motion shots.2",
                    ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000023/16084899-clap-film-of-cinema-action-genre-clapperboard-text-illustration-.jpg?fj=12",
                },
                new Genre()
                {
                    Name = "Action3",
                    Description = "Movies in the action genre are fast-paced and include a lot of action like fight scenes, chase scenes, and slow-motion shots.3",
                    ImageUrl = "https://previews.123rf.com/images/pedrolieb/pedrolieb1210/pedrolieb121000023/16084899-clap-film-of-cinema-action-genre-clapperboard-text-illustration-.jpg?fj=13",
                },
            };
        }

        private DbContextOptions<InfinityCinemaDbContext> PrepareOptionsForDbContext(int testCount)
        {
            return new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: $"GenreServiceTestDb{testCount}")
                .Options;
        }

        private void InitializeMapper() => AutoMapperConfig.RegisterMappings(typeof(GenreViewModel).Assembly);
    }
}
