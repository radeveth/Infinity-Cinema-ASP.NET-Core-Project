namespace InfinityCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Data.Models.Enums;
    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.ApplicationUserMoviesService;
    using InfinityCinema.Services.Data.CountriesService;
    using InfinityCinema.Services.Data.DirectorsService;
    using InfinityCinema.Services.Data.ImagesService;
    using InfinityCinema.Services.Data.LanguagesService;
    using InfinityCinema.Services.Data.MovieActorsService;
    using InfinityCinema.Services.Data.MovieGenresService;
    using InfinityCinema.Services.Data.MovieLanguagesService;
    using InfinityCinema.Services.Data.MoviePlatformsService;
    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Services.Data.MoviesService.Models;
    using InfinityCinema.Services.Data.MovieUserCommentsService;
    using InfinityCinema.Services.Data.PlatformsService;
    using InfinityCinema.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class MovieServiceTests
    {
        public MovieServiceTests()
        {
            this.InitializeMapper();
        }

        // Test #1
        [Fact]
        public async Task CreateAsyncTest()
        {
            // Arrange
            int expectedMoviesCountInDatabase = 1;
            InfinityCinemaDbContext dbContext = this.PrepareDbContext(1);
            MovieService movieService = this.SeedServiceDependencies(dbContext);

            // Act
            MovieFormModel movieForm = new MovieFormModel() { Name = "Creed 2", Description = "Creed 2 Description", DateOfReleased = DateTime.UtcNow, Duration = "2h 10m", Language = "English", Resolution = Resolution.HD, };
            await movieService.CreateAsync<MovieListingViewModel>(movieForm, 1, 1, "111");

            // Assert
            Assert.Equal(expectedMoviesCountInDatabase, dbContext.Movies.Count());
        }

        // Test #2
        [Fact]
        public async Task DetailsTest()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = this.PrepareDbContext(2);
            MovieService movieService = this.SeedServiceDependencies(dbContext);

            List<Country> countries = this.SeedCountries();
            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();

            List<Movie> movies = this.SeedingMovies();
            await dbContext.Movies.AddRangeAsync(movies);
            await dbContext.SaveChangesAsync();

            string expectedMovieName = movies.First().Name;
            string expectedMovieDuration = movies.First().Duration;
            string expectedMovieDescription = movies.First().Description;
            DateTime expectedMovieDateOfReleassed = movies.First().DateOfReleased;

            // Act
            MovieDetailsServiceModel movieDetails = movieService.Details(1);

            // Assert
            Assert.Equal(expectedMovieName, movieDetails.Name);
            Assert.Equal(expectedMovieDuration, movieDetails.Duration);
            Assert.Equal(expectedMovieDescription, movieDetails.Description);
            Assert.Equal(expectedMovieDateOfReleassed, movieDetails.DateOfReleased);
        }

        // Test #3
        [Fact]
        public async Task DetailsShouldThrowNullReferenceExceptionIfMovieWithGivenIdDoesNotExist()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = this.PrepareDbContext(3);
            MovieService movieService = this.SeedServiceDependencies(dbContext);

            List<Country> countries = this.SeedCountries();
            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();

            List<Movie> movies = this.SeedingMovies();
            await dbContext.Movies.AddRangeAsync(movies);
            await dbContext.SaveChangesAsync();

            // Act && Assert
            Assert.Throws<NullReferenceException>(() => movieService.Details(movies.First().Id + 100));
        }

        // Test #4
        [Fact]
        public async Task AllTest()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = this.PrepareDbContext(4);
            MovieService movieService = this.SeedServiceDependencies(dbContext);

            List<Country> countries = this.SeedCountries();
            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();

            List<Movie> movies = this.SeedingMovies();
            await dbContext.Movies.AddRangeAsync(movies);
            await dbContext.SaveChangesAsync();

            // Act
            AllMoviesQueryModel moviesQueryModel = movieService.All();

            // Assert
            Assert.Equal(dbContext.Movies.Count(), moviesQueryModel.Movies.Count());
        }

        // Test #5
        [Fact]
        public async Task GetTopThreeRatedMoviesShouldOrderMoviesByIdIfAllMoviesDoNotHaveGivenStarRatingAndMapThemToMovieHomeViewModel()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = this.PrepareDbContext(5);
            MovieService movieService = this.SeedServiceDependencies(dbContext);

            List<Country> countries = this.SeedCountries();
            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();

            List<Movie> movies = this.SeedingMovies();
            await dbContext.Movies.AddRangeAsync(movies);
            await dbContext.SaveChangesAsync();

            // Act
            List<MovieHomeViewModel> movieHomeModels = movieService.GetTopThreeRatedMovies();

            // Assert
            Assert.Equal(3, movieHomeModels.Count);
            Assert.Equal(movies.First().Id, movieHomeModels.Last().Id);
            Assert.Equal(movies.Last().Id, movieHomeModels.First().Id);
        }

        // Test #6
        [Fact]
        public async Task DeleteAsyncShouldRemoveAllRealtionBetweenMoviesAndOtherTablesAndRemovetMovie()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = this.PrepareDbContext(6);
            MovieService movieService = this.SeedServiceDependencies(dbContext);

            List<Country> countries = this.SeedCountries();
            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();

            List<Movie> movies = this.SeedingMovies();
            await dbContext.Movies.AddRangeAsync(movies);
            await dbContext.SaveChangesAsync();

            int expectedMoviesCountInDatabaseAfterDeleting = dbContext.Movies.Count() - 1;

            // Act
            await movieService.DeleteAsync(new DeleteMovieServiceModel()
            {
                Id = 1,
            });

            // Assert
            Assert.Equal(expectedMoviesCountInDatabaseAfterDeleting, dbContext.Movies.Count());
        }

        // Test #7
        [Fact]
        public async Task DeleteAsyncShouldThrowNullReferenceExceptionIfMovieWithGivenIdDoesNotExist()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = this.PrepareDbContext(7);
            MovieService movieService = this.SeedServiceDependencies(dbContext);

            List<Country> countries = this.SeedCountries();
            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();

            List<Movie> movies = this.SeedingMovies();
            await dbContext.Movies.AddRangeAsync(movies);
            await dbContext.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await movieService.DeleteAsync(new DeleteMovieServiceModel() { Id = movies.First().Id + 100 }));
        }

        private List<Movie> SeedingMovies()
        {
            return new List<Movie>()
            {
                new Movie()
                {
                    Id = 1,
                    Name = "The Expendables 3",
                    Duration = "2h 11m",
                    Description = "Barney (Stallone), Christmas (Statham) and the rest of the team comes face-to-face with Conrad Stonebanks (Gibson), who years ago co-founded The Expendables with Barney. Stonebanks subsequently became a ruthless arms trader and someone who Barney was forced to kill - or so he thought. Stonebanks, who eluded death once before, now is making it his mission to end The Expendables -- but Barney has other plans.",
                    DateOfReleased = DateTime.UtcNow,
                    CountryId = 1,
                },
                new Movie()
                {
                    Id = 2,
                    Name = "Creed ||",
                    Duration = "2h 10m",
                    Description = "Years after Adonis Creed made a name for himself under Rocky Balboa's mentorship, the young boxer becomes the Heavyweight Champion of the World. While life is good with that victory and his marriage to Bianca, trouble comes to Philadelphia when Ivan Drago, the Russian boxer who killed Adonis' father, Apollo, arrives with his son, Viktor, to challenge Adonis.",
                    DateOfReleased = DateTime.UtcNow,
                    CountryId = 2,
                },
                new Movie()
                {
                    Id = 3,
                    Name = "Rocky II",
                    Duration = "1h 59m",
                    Description = "Rocky Balboa is enjoying life. He has a lovely wife, Adrian, had a successful fight with Apollo Creed and is able to enjoy the money he earned from the fight and a new endorsement deal. Unfortunately, Rocky becomes embarrassed when failing to complete an advert and ends up working in a meat packing company. He believes that he will no longer have a career as a boxer. Apollo wants to rematch with Rocky to prove all his critics wrong that he can beat Rocky.",
                    DateOfReleased = DateTime.UtcNow,
                    CountryId = 3,
                },
            };
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

        private MovieService SeedServiceDependencies(InfinityCinemaDbContext dbContext)
        {
            ImageService imageService = new ImageService(dbContext);
            ActorService actorService = new ActorService(dbContext);
            CountryService countryService = new CountryService(dbContext);
            PlatformService platformService = new PlatformService(dbContext);
            DirectorService directorService = new DirectorService(dbContext);
            LanguageService languageService = new LanguageService(dbContext);
            MovieActorService movieActorService = new MovieActorService(dbContext);
            MovieGenreService movieGenreService = new MovieGenreService(dbContext);
            MovieLanguageService movieLanguageService = new MovieLanguageService(dbContext);
            MoviePlatformService moviePlatformService = new MoviePlatformService(dbContext);
            MovieUserCommentService movieUserCommentsService = new MovieUserCommentService(dbContext);
            ApplicationUserMovieService applicationUserMovieService = new ApplicationUserMovieService(dbContext);

            return new MovieService(dbContext,
                imageService,
                actorService,
                countryService,
                platformService,
                directorService,
                languageService,
                movieActorService,
                movieGenreService,
                movieLanguageService,
                moviePlatformService,
                movieUserCommentsService,
                applicationUserMovieService);
        }

        private InfinityCinemaDbContext PrepareDbContext(int testCount)
        {
            DbContextOptions<InfinityCinemaDbContext> dbContextOptionsBuilder =
                new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: $"MovieServiceTestDb{testCount}").Options;

            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(dbContextOptionsBuilder);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        private void InitializeMapper() => AutoMapperConfig.RegisterMappings(typeof(MovieDetailsServiceModel).Assembly);
    }
}
