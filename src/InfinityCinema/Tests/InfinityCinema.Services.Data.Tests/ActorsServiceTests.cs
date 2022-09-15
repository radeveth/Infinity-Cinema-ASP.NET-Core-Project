namespace InfinityCinema.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.ActorsService.Models;
    using InfinityCinema.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ActorsServiceTests
    {
        public ActorsServiceTests()
        {
            this.InitializeMapper();
        }

        // Test #1
        [Fact]
        public async Task CreateAsyncShouldMapGivenActorFormModelToActorAndAddToDatabase()
        {
            // Arrange
            int expectedActorsInDatabaseCount = 1;
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(1));
            ActorService actorService = new ActorService(dbContext);

            ActorFormModel actorForm = new ActorFormModel()
            {
                FullName = "Sylvester Stallone",
                ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg",
                InformationLink = "https://www.imdb.com/name/nm0000230/",
            };

            // Act
            await actorService.CreateAsync<ActorViewModel>(actorForm);
            int actualCountOfActorsInDatabase = actorService.All().Count();

            // Assert
            Assert.Equal(expectedActorsInDatabaseCount, actualCountOfActorsInDatabase);
        }

        // Test #2
        [Fact]
        public async Task CreateAsyncShouldMapGivenActorFormModelToActorAndAddToDatabaseManyTimes()
        {
            // Arrange
            int expectedActorsInDatabaseCount = 5;
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(2));
            ActorService actorService = new ActorService(dbContext);

            List<ActorFormModel> actorsForm = this.SeedingInformationForActorFormModel();

            // Act
            foreach (var actorForm in actorsForm)
            {
                await actorService.CreateAsync<ActorViewModel>(actorForm);
            }

            int actualCountOfActorsInDatabase = actorService.All().Count();

            // Assert
            Assert.Equal(expectedActorsInDatabaseCount, actualCountOfActorsInDatabase);
        }

        // Test #3
        [Fact]
        public async Task GetActorByNameShouldReturnActorViewModelWithGivenNameOfExistingActor()
        {
            // Arrange
            Actor targetActor = new Actor()
            {
                FirstName = "Sylvester1",
                LastName = "Stallone1",
                ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg1",
                InformationLink = "https://www.imdb.com/name/nm0000230/1",
            };
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(3));
            ActorService actorService = new ActorService(dbContext);

            List<Actor> actors = this.SeedingInformationForActor();
            await dbContext.Actors.AddRangeAsync(actors);
            await dbContext.SaveChangesAsync();

            // Act
            ActorViewModel actorViewModel = actorService.GetActorByNames($"{targetActor.FirstName} {targetActor.LastName}");

            // Assert
            Assert.Equal($"{targetActor.FirstName} {targetActor.LastName}".ToString(), actorViewModel.FullName.ToString());
            Assert.Equal(targetActor.ImageUrl, actorViewModel.ImageUrl);
            Assert.Equal(targetActor.InformationLink, actorViewModel.InformationLink);
        }

        // Test #4
        [Fact]
        public async Task GetActorByNameShouldReturnNullIfThereAreNoFoundActorsByGivenName()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(4));
            ActorService actorService = new ActorService(dbContext);

            List<Actor> actors = this.SeedingInformationForActor();
            await dbContext.Actors.AddRangeAsync(actors);
            await dbContext.SaveChangesAsync();

            // Act
            ActorViewModel actorViewModel = actorService.GetActorByNames("NoExisting Actor");

            // Assert
            Assert.True(actorViewModel == null);
        }

        // Test #5
        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnGivenViewModelByGivenActorId()
        {
            // Arrange
            Actor targetActor = new Actor()
            {
                FirstName = "Sylvester1",
                LastName = "Stallone1",
                ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg1",
                InformationLink = "https://www.imdb.com/name/nm0000230/1",
            };
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(5));
            ActorService actorService = new ActorService(dbContext);

            List<Actor> actors = this.SeedingInformationForActor();
            await dbContext.Actors.AddRangeAsync(actors);
            await dbContext.SaveChangesAsync();

            // Act
            ActorViewModel actorViewModel = actorService.GetViewModelByIdAsync<ActorViewModel>(1);

            // Assert
            Assert.Equal($"{targetActor.FirstName} {targetActor.LastName}", actorViewModel.FullName);
            Assert.Equal(targetActor.ImageUrl, actorViewModel.ImageUrl);
            Assert.Equal(targetActor.InformationLink, actorViewModel.InformationLink);
        }

        // Test #6
        [Fact]
        public async Task GetViewModelByIdAsyncShouldReturnDefaultValueOfGenereicTypeIfGivenActorIdDoesNotExist()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(6));
            ActorService actorService = new ActorService(dbContext);

            List<Actor> actors = this.SeedingInformationForActor();
            await dbContext.Actors.AddRangeAsync(actors);
            await dbContext.SaveChangesAsync();

            // Act
            ActorViewModel actorViewModel = actorService.GetViewModelByIdAsync<ActorViewModel>(6);

            // Assert
            Assert.True(actorViewModel == null);
        }

        // Test #7
        [Fact]
        public async Task DeleteAsyncShouldWorkCorrectWithGivenExistingActorId()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(7));
            ActorService actorService = new ActorService(dbContext);
            List<Actor> actors = this.SeedingInformationForActor();

            await dbContext.Actors.AddRangeAsync(actors);
            await dbContext.SaveChangesAsync();

            int expectedActorsCount = dbContext.Actors.Count() - 1;

            // Act
            await actorService.DeleteAsync(1);

            // Assert
            Assert.Equal(expectedActorsCount, dbContext.Actors.Count());

            expectedActorsCount = dbContext.Actors.Count() - 1;
            await actorService.DeleteAsync(2);

            Assert.Equal(expectedActorsCount, dbContext.Actors.Count());
        }

        private List<Actor> SeedingInformationForActor()
        {
            return new List<Actor>()
            {
                new Actor()
                {
                    FirstName = "Sylvester1",
                    LastName = "Stallone1",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg1",
                    InformationLink = "https://www.imdb.com/name/nm0000230/1",
                },
                new Actor()
                {
                    FirstName = "Sylvester2",
                    LastName = "Stallone2",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg2",
                    InformationLink = "https://www.imdb.com/name/nm0000230/2",
                },
                new Actor()
                {
                    FirstName = "Sylvester3",
                    LastName = "Stallone3",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg3",
                    InformationLink = "https://www.imdb.com/name/nm0000230/3",
                },
                new Actor()
                {
                    FirstName = "Sylvester4",
                    LastName = "Stallone4",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg4",
                    InformationLink = "https://www.imdb.com/name/nm0000230/4",
                },
                new Actor()
                {
                    FirstName = "Sylvester5",
                    LastName = "Stallone5",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg5",
                    InformationLink = "https://www.imdb.com/name/nm0000230/5",
                },
            };
        }

        private List<ActorFormModel> SeedingInformationForActorFormModel()
        {
            return new List<ActorFormModel>()
            {
                new ActorFormModel()
                {
                    FullName = "Sylvester1 Stallone1",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg1",
                    InformationLink = "https://www.imdb.com/name/nm0000230/1",
                },
                new ActorFormModel()
                {
                    FullName = "Sylvester2 Stallone2",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg2",
                    InformationLink = "https://www.imdb.com/name/nm0000230/2",
                },
                new ActorFormModel()
                {
                    FullName = "Sylvester3 Stallone3",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg3",
                    InformationLink = "https://www.imdb.com/name/nm0000230/3",
                },
                new ActorFormModel()
                {
                    FullName = "Sylvester4 Stallone4",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg4",
                    InformationLink = "https://www.imdb.com/name/nm0000230/4",
                },
                new ActorFormModel()
                {
                    FullName = "Sylvester5 Stallone5",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg5",
                    InformationLink = "https://www.imdb.com/name/nm0000230/5",
                },
            };
        }

        private DbContextOptions<InfinityCinemaDbContext> PrepareOptionsForDbContext(int testCount)
        {
            return new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: $"ActorsTestDb{testCount}").Options;
        }

        private void InitializeMapper() => AutoMapperConfig.RegisterMappings(typeof(ActorViewModel).Assembly);
    }
}
