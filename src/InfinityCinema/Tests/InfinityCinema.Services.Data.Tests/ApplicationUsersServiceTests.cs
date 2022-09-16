namespace InfinityCinema.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Data.Models.Enums;
    using InfinityCinema.Services.Data.ApplicationUsersService;
    using InfinityCinema.Services.Data.ApplicationUsersService.Models;
    using InfinityCinema.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ApplicationUsersServiceTests
    {
        //public ApplicationUsersServiceTests()
        //{
        //    this.InitializeMapper();
        //}

        //// Test #1
        //[Fact]
        //public async Task SaveMovieToWatchLaterAsyncTest()
        //{
        //    // Arrange
        //    int expectedResult = 1;

        //    InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(1));
        //    ApplicationUserService applicationUserService = new ApplicationUserService(dbContext);

        //    List<ApplicationUser> applicationUsers = this.SeedingUsers();
        //    await dbContext.Users.AddRangeAsync(applicationUsers);
        //    await dbContext.SaveChangesAsync();

        //    List<Movie> movies = this.SeedingMovies();
        //    await dbContext.Movies.AddRangeAsync(movies);
        //    await dbContext.SaveChangesAsync();

        //    // Act
        //    await applicationUserService.SaveMovieToWatchLaterAsync(movies.First().Id, applicationUsers.First().Id);

        //    // Assert
        //    Assert.True(expectedResult == 1);
        //}

        //// Test #2
        //[Fact]
        //public async Task RemoveMovieFromWatchLaterAsyncsTest()
        //{
        //    // Arrange
        //    int expectedResult = 0;

        //    InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(2));
        //    ApplicationUserService applicationUserService = new ApplicationUserService(dbContext);

        //    List<ApplicationUser> applicationUsers = this.SeedingUsers();
        //    await dbContext.Users.AddRangeAsync(applicationUsers);
        //    await dbContext.SaveChangesAsync();

        //    List<Movie> movies = this.SeedingMovies();
        //    await dbContext.Movies.AddRangeAsync(movies);
        //    await dbContext.SaveChangesAsync();

        //    // Act
        //    await applicationUserService.SaveMovieToWatchLaterAsync(movies.First().Id, applicationUsers.First().Id);
        //    await applicationUserService.RemoveMovieFromWatchLaterAsync(movies.First().Id, applicationUsers.First().Id);

        //    // Assert
        //    Assert.True(expectedResult == 0);
        //}

        //// Test #3
        //[Fact]
        //public async Task RateMovieTest()
        //{
        //    // Arrange
        //    int expectedResult = 1;

        //    InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(3));
        //    ApplicationUserService applicationUserService = new ApplicationUserService(dbContext);

        //    List<ApplicationUser> applicationUsers = this.SeedingUsers();
        //    await dbContext.Users.AddRangeAsync(applicationUsers);
        //    await dbContext.SaveChangesAsync();

        //    List<Movie> movies = this.SeedingMovies();
        //    await dbContext.Movies.AddRangeAsync(movies);
        //    await dbContext.SaveChangesAsync();

        //    // Act
        //    await applicationUserService.RateMovie(movies.First().Id, applicationUsers.First().Id, 6);

        //    // Assert
        //    Assert.Equal(expectedResult, dbContext.MovieUserStarRatings.Count());
        //}

        //// Test #4
        //[Fact]
        //public async Task GetViewModelByIdTest()
        //{
        //    // Arrange
        //    ApplicationUser targetApplicationUser = new ApplicationUser()
        //    {
        //        FullName = "User 1",
        //        Email = "user1@gmail.com",
        //        Gender = Gender.Male,
        //    };

        //    InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(4));
        //    ApplicationUserService applicationUserService = new ApplicationUserService(dbContext);

        //    List<ApplicationUser> applicationUsers = this.SeedingUsers();
        //    await dbContext.Users.AddRangeAsync(applicationUsers);
        //    await dbContext.SaveChangesAsync();

        //    // Act
        //    ApplicationUserViewModel viewModel = applicationUserService
        //        .GetViewModelById<ApplicationUserViewModel>(applicationUsers.First().Id);

        //    Assert.Equal(targetApplicationUser.FullName, viewModel.FullName);
        //    Assert.Equal(targetApplicationUser.Gender, viewModel.Gender);
        //}

        //// Test #5
        //[Fact]
        //public async Task GetUsersIdsThatAreSaveGivenMovie()
        //{
        //    // Arrange
        //    InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(2));
        //    ApplicationUserService applicationUserService = new ApplicationUserService(dbContext);

        //    List<ApplicationUser> applicationUsers = this.SeedingUsers();
        //    await dbContext.Users.AddRangeAsync(applicationUsers);
        //    await dbContext.SaveChangesAsync();

        //    List<Movie> movies = this.SeedingMovies();
        //    await dbContext.Movies.AddRangeAsync(movies);
        //    await dbContext.SaveChangesAsync();

        //    await applicationUserService.SaveMovieToWatchLaterAsync(movies.First().Id, applicationUsers.First().Id);
        //    int expectedResult = dbContext.ApplicationUserMovies.Count();

        //    // Act
        //    int actualResult = applicationUserService.GetUsersIdsThatAreSaveGivenMovie(movies.First().Id).Count();

        //    // Assert
        //    Assert.Equal(expectedResult, actualResult);
        //}

        //private List<ApplicationUser> SeedingUsers()
        //{
        //    return new List<ApplicationUser>()
        //    {
        //        new ApplicationUser()
        //        {
        //            FullName = "User 1",
        //            Email = "user1@gmail.com",
        //            Gender = Gender.Male,
        //        },
        //        new ApplicationUser()
        //        {
        //            FullName = "User 2",
        //            Email = "user2@gmail.com",
        //            Gender = Gender.Female,
        //        },
        //        new ApplicationUser()
        //        {
        //            FullName = "User 3",
        //            Email = "user3@gmail.com",
        //            Gender = Gender.Male,
        //        },
        //    };
        //}

        //private List<Movie> SeedingMovies()
        //{
        //    return new List<Movie>()
        //    {
        //        new Movie()
        //        {
        //            Name = "The Expendables 3",
        //            Duration = "2h 11m",
        //            Description = "Barney (Stallone), Christmas (Statham) and the rest of the team comes face-to-face with Conrad Stonebanks (Gibson), who years ago co-founded The Expendables with Barney. Stonebanks subsequently became a ruthless arms trader and someone who Barney was forced to kill - or so he thought. Stonebanks, who eluded death once before, now is making it his mission to end The Expendables -- but Barney has other plans.",
        //        },
        //        new Movie()
        //        {
        //            Name = "Creed ||",
        //            Duration = "2h 10m",
        //            Description = "Years after Adonis Creed made a name for himself under Rocky Balboa's mentorship, the young boxer becomes the Heavyweight Champion of the World. While life is good with that victory and his marriage to Bianca, trouble comes to Philadelphia when Ivan Drago, the Russian boxer who killed Adonis' father, Apollo, arrives with his son, Viktor, to challenge Adonis.",
        //        },
        //        new Movie()
        //        {
        //            Name = "Rocky II",
        //            Duration = "1h 59m",
        //            Description = "Rocky Balboa is enjoying life. He has a lovely wife, Adrian, had a successful fight with Apollo Creed and is able to enjoy the money he earned from the fight and a new endorsement deal. Unfortunately, Rocky becomes embarrassed when failing to complete an advert and ends up working in a meat packing company. He believes that he will no longer have a career as a boxer. Apollo wants to rematch with Rocky to prove all his critics wrong that he can beat Rocky.",
        //        },
        //    };
        //}

        //private DbContextOptions<InfinityCinemaDbContext> PrepareOptionsForDbContext(int testCount)
        //{
        //    return new DbContextOptionsBuilder<InfinityCinemaDbContext>()
        //        .UseInMemoryDatabase(databaseName: $"ApplicationUserTestDb{testCount}").Options;
        //}

        //private void InitializeMapper() => AutoMapperConfig.RegisterMappings(typeof(ApplicationUserViewModel).Assembly);
    }
}
