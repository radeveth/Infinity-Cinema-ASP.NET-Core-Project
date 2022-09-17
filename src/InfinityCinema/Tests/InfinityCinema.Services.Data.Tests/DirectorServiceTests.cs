namespace InfinityCinema.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.DirectorsService;
    using InfinityCinema.Services.Data.DirectorsService.Models;
    using InfinityCinema.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class DirectorServiceTests
    {
        public DirectorServiceTests()
        {
            this.InitializeMapper();
        }

        // Test #1
        [Fact]
        public async Task CreateAsyncShouldMapDirectorFormModelToDirectorEntityAndAddNewDirectorToDatabase()
        {
            // Arrange
            int expectedDirectorsCountInDatabase = 1;
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(1));
            DirectorService directorsService = new DirectorService(dbContext);

            // Act
            await directorsService.CreateAsync<DirectorViewModel>(new DirectorFormModel()
            {
                FullName = "Patrick Hughes",
                InformationUrl = "https://www.imdb.com/name/nm0400850/?ref_=tt_ov_dr",
            });

            // Assert
            Assert.Equal(expectedDirectorsCountInDatabase, dbContext.Directors.Count());
        }

        // Test #2
        [Fact]
        public async Task GetViewModelByGivenFullNameTest()
        {
            // Arrange
            DirectorViewModel expectedDorectorViewModel = new DirectorViewModel()
            {
                FullName = "Patrick1 Hughes1",
                InformationLink = "https://www.imdb.com/name/nm0400850/?ref_=tt_ov_dr1",
            };

            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(2));
            DirectorService directorService = new DirectorService(dbContext);

            List<Director> directors = this.SeedDirectors();
            await dbContext.AddRangeAsync(directors);
            await dbContext.SaveChangesAsync();

            // Act
            DirectorViewModel directorViewModel = directorService
                .GetViewModelByGivenFullName<DirectorViewModel>(expectedDorectorViewModel.FullName);

            // Assert
            Assert.Equal(expectedDorectorViewModel.FullName, directorViewModel.FullName);
            Assert.Equal(expectedDorectorViewModel.InformationLink, directorViewModel.InformationLink);
        }

        // Test #3
        [Fact]
        public async Task GetViewModelByIdTest()
        {
            // Arrange
            DirectorViewModel expectedDorectorViewModel = new DirectorViewModel()
            {
                FullName = "Patrick1 Hughes1",
                InformationLink = "https://www.imdb.com/name/nm0400850/?ref_=tt_ov_dr1",
            };

            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(3));
            DirectorService directorService = new DirectorService(dbContext);

            List<Director> directors = this.SeedDirectors();
            await dbContext.AddRangeAsync(directors);
            await dbContext.SaveChangesAsync();

            // Act
            DirectorViewModel directorViewModel = directorService
                .GetViewModelById<DirectorViewModel>(directors.First().Id);

            // Assert
            Assert.Equal(expectedDorectorViewModel.FullName, directorViewModel.FullName);
            Assert.Equal(expectedDorectorViewModel.InformationLink, directorViewModel.InformationLink);
        }

        private List<Director> SeedDirectors()
        {
            return new List<Director>()
            {
                new Director()
                {
                    FirstName = "Patrick1",
                    LastName = "Hughes1",
                    InformationUrl = "https://www.imdb.com/name/nm0400850/?ref_=tt_ov_dr1",
                },
                new Director()
                {
                    FirstName = "Patrick2",
                    LastName = "Hughes2",
                    InformationUrl = "https://www.imdb.com/name/nm0400850/?ref_=tt_ov_dr2",
                },
                new Director()
                {
                    FirstName = "Patrick3",
                    LastName = "Hughes3",
                    InformationUrl = "https://www.imdb.com/name/nm0400850/?ref_=tt_ov_dr3",
                },
            };
        }

        private DbContextOptions<InfinityCinemaDbContext> PrepareOptionsForDbContext(int testCount)
        {
            return new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: $"DirectorServiceTestDb{testCount}")
                .Options;
        }

        private void InitializeMapper() => AutoMapperConfig.RegisterMappings(typeof(DirectorViewModel).Assembly);
    }
}
