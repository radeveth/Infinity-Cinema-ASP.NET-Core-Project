namespace InfinityCinema.Services.Data.MoviesService
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.CountriesService;
    using InfinityCinema.Services.Data.DirectorsService;
    using InfinityCinema.Services.Data.ImagesService;
    using InfinityCinema.Services.Data.PlatformsService;
    using Microsoft.AspNetCore.Identity;


    public class MovieService : IMovieService
    {
        private readonly InfinityCinemaDbContext dbContext;
        private readonly IDirectorService directorService;
        private readonly IImageService imageService;
        private readonly ICountryService countryService;
        private readonly IActorService actorService;
        private readonly IPlatformService platformService;

        private readonly UserManager<ApplicationUser> userManager;

        public MovieService
            (InfinityCinemaDbContext dbContext,
            IDirectorService directorService,
            IImageService imageService,
            ICountryService countryService,
            UserManager<ApplicationUser> userManager,
            IPlatformService platformService)
        {
            this.dbContext = dbContext;
            this.directorService = directorService;
            this.imageService = imageService;
            this.countryService = countryService;
            this.userManager = userManager;
            this.platformService = platformService;
        }

        public async Task<string> CreateMovieAsync(CreateMovieServiceModel createMovieModel, ClaimsPrincipal user)
        {
            // Get Director Id
            DirectorFormModel directorFormModel = createMovieModel.OverallMovieInformation.Director;
            int directorId = await this.directorService.GetDirectorIdAsync(directorFormModel);

            // Get Country Id
            string countryName = createMovieModel.OverallMovieInformation.Country;
            if (!this.countryService.CheckIfCountryExist(countryName))
            {
                await this.countryService.CreateAsync(countryName);
            }

            int countryId = this.countryService.GetCountryIdByGivenName(countryName);

            // Get UserId
            string userId = this.GetUserId(user);

            // Create Movie
            MovieFormModel movieFormModel = createMovieModel.OverallMovieInformation;
            Movie movie = await this.CreateAsync(movieFormModel, directorId, userId);
            int movieId = movie.Id;

            // Create image for particular movie
            IEnumerable<ImageFormModel> images = createMovieModel.Images;
            foreach (ImageFormModel image in images)
            {
                image.MovieId = movieId;
                await this.imageService.CreateAsync(image);
            }

            // Match genres with particular movie
            IEnumerable<int> genresIds = createMovieModel.OverallMovieInformation.GenresId;
            await this.MatchGenresWithMovie(movieId, genresIds);

            // Create actors for particular movie
            IEnumerable<ActorFormModel> actors = createMovieModel.Actors;
            ICollection<int> actorsIds = new List<int>();
            foreach (ActorFormModel actorFormModel in actors)
            {
                Actor actor = await this.actorService.CreateAsync(actorFormModel);
                actorsIds.Add(actor.Id);
            }

            // Match actors with particular movie !!!!!!!!!!!!!!!!!!!!!! TODO: Method like MovieGenres !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            foreach (int actorId in actorsIds)
            {
                await this.dbContext.MovieActors.AddAsync(new MovieActor() { MovieId = movieId, ActorId = actorId});
            }

            // Create Platforms for particular movie
            IEnumerable<PlatformFormModel> platforms = createMovieModel.Platforms;
            ICollection<int> platformsIds = new List<int>();
            foreach (PlatformFormModel platformFormModel in platforms)
            {
                Platform platform = await this.platformService.CreateAsync(platformFormModel);
                platformsIds.Add(platform.Id);
            }

            // Match platforms with particular movie !!!!!!!!!!!!!!!!!!!!!! TODO: Method like MovieGenres !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            foreach (int platformId in platformsIds)
            {
                await this.dbContext.MoviePlatform.AddAsync(new MoviePlatform() { MovieId = movieId, PlatformId = platformId });
            }

            return null;
        }

        private async Task MatchGenresWithMovie(int movieId, IEnumerable<int> genresIds)
        {
            ICollection<MovieGenre> movieGenres = new HashSet<MovieGenre>();

            foreach (int genreId in genresIds)
            {
                movieGenres.Add(new MovieGenre() { MovieId = movieId, GenreId = genreId });
            }

            await this.dbContext.MovieGenres.AddRangeAsync(movieGenres);
        }

        public async Task<Movie> CreateAsync(MovieFormModel movieFormModel, int directorId, string userId)
        {
            Movie movie = new Movie()
            {
                Name = movieFormModel.Name,
                DateOfReleased = movieFormModel.DateOfReleased,
                Resolution = movieFormModel.Resolution,
                Description = movieFormModel.Description,
                TrailerPath = movieFormModel.TrailerPath,
                Duration = movieFormModel.Duration,
                DirectorId = directorId,
                UserId = userId,
            };

            await this.dbContext.AddAsync(movie);
            await this.dbContext.SaveChangesAsync();

            return movie;
        }

        private string GetUserId(ClaimsPrincipal user)
        {
            return this.userManager.GetUserId(user);
        }
    }
}
