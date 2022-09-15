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

        [Fact]
        public async Task CreateAsyncShouldTakeGivenFormModelAndCreateNewActorForDatabaseTest()
        {
            // Arrange
            DbContextOptionsBuilder<InfinityCinemaDbContext> optionsBuilder = new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "ActorsTestDb");
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(optionsBuilder.Options);
            ActorService service = new ActorService(dbContext);

            ActorFormModel actorForm = new ActorFormModel()
            {
                FullName = "Sylvester Stallone",
                ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg",
                InformationLink = "https://www.imdb.com/name/nm0000230/",
            };

            // Act
            await service.CreateAsync<ActorViewModel>(actorForm);
            int count = service.All().Count();
            ActorViewModel actorViewModel = service.All().FirstOrDefault();

            // Assert
            Assert.Equal(1, count);
            Assert.Equal(actorViewModel.FullName, dbContext.Actors.To<ActorViewModel>().FirstOrDefault().FullName);
            Assert.Equal(actorViewModel.ImageUrl, dbContext.Actors.To<ActorViewModel>().FirstOrDefault().ImageUrl);
            Assert.Equal(actorViewModel.InformationLink, dbContext.Actors.To<ActorViewModel>().FirstOrDefault().InformationLink);
        }

        [Fact]
        public async Task AllShouldReturnAllExistingActorsInDatabaseMappedToViewModel()
        {
            // Arrange
            DbContextOptionsBuilder<InfinityCinemaDbContext> optionsBuilder = new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "ActorsTestDb");
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(optionsBuilder.Options);
            ActorService service = new ActorService(dbContext);

            List<ActorFormModel> actorsForm = new List<ActorFormModel>()
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
            };

            foreach (var actorForm in actorsForm)
            {
                await service.CreateAsync<ActorViewModel>(actorForm);
            }

            // Act
            IEnumerable<ActorViewModel> actors = service.All();

            // Assert
            Assert.Equal(dbContext.Actors.Count(), actors.Count());
            Assert.Equal(dbContext.Actors.To<ActorViewModel>().ToString(), actors.ToString());
        }

        [Fact]
        public async Task GetActorByNamesShouldReturnViewModelForActorWithGivenExistingActorNamesInDatabase()
        {
            // Arrange
            DbContextOptionsBuilder<InfinityCinemaDbContext> optionsBuilder = new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "ActorsTestDb");
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(optionsBuilder.Options);
            ActorService service = new ActorService(dbContext);
            List<Actor> actors = new List<Actor>()
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
            };

            await dbContext.Actors.AddRangeAsync(actors);
            await dbContext.SaveChangesAsync();

            // Act
            ActorViewModel targetActor = service.GetActorByNames($"{actors[0].FirstName} {actors[0].LastName}");

            // Arrange
            Assert.Equal(actors.Count(), dbContext.Actors.Count());
            Assert.Equal(dbContext.Actors.To<ActorViewModel>().FirstOrDefault().ToString(), targetActor.ToString());
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteActorWithGivenIdFromDatabase()
        {
            // Arrange
            DbContextOptionsBuilder<InfinityCinemaDbContext> optionsBuilder = new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: "ActorsTestDb");
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(optionsBuilder.Options);
            ActorService service = new ActorService(dbContext);

            Actor actorToDelete = new Actor()
            {
                FirstName = "Sylvester1",
                LastName = "Stallone1",
                ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTQwMTk3NDU2OV5BMl5BanBnXkFtZTcwNTA3MTI0Mw@@._V1_UY317_CR6,0,214,317_AL_.jpg1",
                InformationLink = "https://www.imdb.com/name/nm0000230/1",
            };

            List<Actor> actors = new List<Actor>()
            {
                actorToDelete,
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
            };

            await dbContext.Actors.AddRangeAsync(actors);
            await dbContext.SaveChangesAsync();

            // Act
            await service.DeleteAsync(actorToDelete.Id);

            // Assert
            Assert.False(dbContext.Actors.Contains(actorToDelete));
            Assert.False(actors.Count() == dbContext.Actors.Count());
            Assert.True(actors.Count() - 1 == dbContext.Actors.Count());
        }

        private void InitializeMapper() => AutoMapperConfig.RegisterMappings(typeof(ActorViewModel).Assembly);
    }
}
