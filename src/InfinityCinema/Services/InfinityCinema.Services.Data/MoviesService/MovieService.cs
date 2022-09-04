namespace InfinityCinema.Services.Data.MoviesService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Common.Repositories;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.ActorsService.Models;
    using InfinityCinema.Services.Data.CountriesService;
    using InfinityCinema.Services.Data.DirectorsService;
    using InfinityCinema.Services.Data.DirectorsService.Models;
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.GenresService.Models;
    using InfinityCinema.Services.Data.ImagesService;
    using InfinityCinema.Services.Data.ImagesService.Models;
    using InfinityCinema.Services.Data.LanguagesService;
    using InfinityCinema.Services.Data.LanguagesService.Models;
    using InfinityCinema.Services.Data.MovieCommentsService;
    using InfinityCinema.Services.Data.MovieCommentsService.Models;
    using InfinityCinema.Services.Data.MoviesService.Enums;
    using InfinityCinema.Services.Data.MoviesService.Models;
    using InfinityCinema.Services.Data.PlatformsService;
    using InfinityCinema.Services.Data.PlatformsService.Models;
    using Microsoft.AspNetCore.Identity;

    public class MovieService : IMovieService
    {
        private readonly IDeletableEntityRepository<Movie> movieRepository;
        private readonly IDeletableEntityRepository<MovieLanguage> movieLanguagesRepository;
        private readonly IDeletableEntityRepository<MovieGenre> movieGenresRepository;

        private readonly InfinityCinemaDbContext dbContext;
        private readonly IDirectorService directorService;
        private readonly IImageService imageService;
        private readonly ICountryService countryService;
        private readonly IActorService actorService;
        private readonly IPlatformService platformService;
        private readonly ILanguageService languageService;
        private readonly IGenreService genreService;
        private readonly IMovieCommentService movieCommentService;

        private readonly UserManager<ApplicationUser> userManager;

        public MovieService(InfinityCinemaDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            IDirectorService directorService,
            IImageService imageService,
            ICountryService countryService,
            IActorService actorService,
            IPlatformService platformService,
            ILanguageService languageService,
            IGenreService genreService,
            IDeletableEntityRepository<Movie> movieRepository,
            IDeletableEntityRepository<MovieLanguage> movieLanguagesRepository,
            IDeletableEntityRepository<MovieGenre> movieGenresRepository,
            IMovieCommentService movieCommentService)
        {
            this.dbContext = dbContext;
            this.directorService = directorService;
            this.imageService = imageService;
            this.countryService = countryService;
            this.actorService = actorService;
            this.userManager = userManager;
            this.platformService = platformService;
            this.languageService = languageService;
            this.genreService = genreService;
            this.movieRepository = movieRepository;
            this.movieLanguagesRepository = movieLanguagesRepository;
            this.movieGenresRepository = movieGenresRepository;
            this.movieCommentService = movieCommentService;
        }

        // Create
        public async Task<Movie> CreateAsync(MovieFormModel movieFormModel, int directorId, int countryId, string userId)
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
                CountryId = countryId,
                UserId = userId,
            };

            await this.dbContext.AddAsync(movie);
            await this.dbContext.SaveChangesAsync();

            return movie;
        }

        public async Task<string> CreateMovieAsync(CreateMovieServiceModel createMovieModel, ClaimsPrincipal user)
        {
            MovieFormModel movieFormModel = createMovieModel.OverallMovieInformation;

            // Get Director Id
            DirectorFormModel directorFormModel = movieFormModel.Director;
            int directorId = this.directorService.GetDirectorIdByGivenFullName(directorFormModel.FullName);

            if (directorId == 0)
            {
                await this.directorService.CreateAsync(directorFormModel);
                directorId = this.directorService.GetDirectorIdByGivenFullName(directorFormModel.FullName);
            }

            // Get Country Id
            string countryName = movieFormModel.Country;
            if (!this.countryService.CheckIfCountryExist(countryName))
            {
                await this.countryService.CreateAsync(countryName);
            }

            int countryId = this.countryService.GetCountryIdByGivenName(countryName);

            // Get UserId
            string userId = this.GetUserId(user);

            // Create Movie
            Movie movie = await this.CreateAsync(movieFormModel, directorId, countryId, userId);
            int movieId = movie.Id;

            // Create Language
            string[] languagesName = movieFormModel.Language
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            ICollection<int> languagesIds = new List<int>();
            foreach (string languageName in languagesName)
            {
                LanguageViewModel language = this.languageService.GetLanguageByName(languageName);

                if (language == null)
                {
                    language = await this.languageService.CreateAsync(languageName);
                }

                languagesIds.Add(language.Id);
            }

            await this.MatchLanguagesWithMovie(movieId, languagesIds);

            // Create images for particular movie
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
                ActorViewModel actor = this.actorService.GetActorByNames(actorFormModel.FullName);

                if (actor == null)
                {
                    actor = await this.actorService.CreateAsync(actorFormModel);
                }

                actorsIds.Add(actor.Id);
            }

            await this.MatchActorsWithMovie(movieId, actorsIds);

            // Create Platforms for particular movie
            IEnumerable<PlatformFormModel> platforms = createMovieModel.Platforms;
            ICollection<int> platformsIds = new List<int>();
            foreach (PlatformFormModel platformFormModel in platforms)
            {
                PlatformViewModel platform = this.platformService.GetPlatformByName(platformFormModel.Name);

                if (platform == null)
                {
                    platform = await this.platformService.CreateAsync(platformFormModel);
                }

                platformsIds.Add(platform.Id);
            }

            await this.MatchPlatformsWithMovie(movieId, platformsIds);

            return "Successfully created movie";
        }

        // Read
        public MovieDetailsViewModel Details(int id)
        {
            Movie movie = this.dbContext.Movies.Find(id);

            IEnumerable<string> images = this.imageService.GetImagesForGivenMovie(id);
            IEnumerable<ActorViewModel> actors = this.actorService.GetActorsForGivenMovie(id);
            IEnumerable<GenreViewModel> genres = this.genreService.GetGenresForParticularMovie(id);
            IEnumerable<string> languages = this.languageService.GetLanguagesForParticularMovie(id);
            IEnumerable<PlatformViewModel> platforms = this.platformService.GetPlatformsForGivenMovie(id);
            DirectorViewModel director = this.directorService.GetDirectorForParticularMovie(movie.DirectorId);
            IEnumerable<string> applicationUsersId = this.dbContext.ApplicationUserMovies.Where(a => a.MovieId == id).Select(a => a.UserId);
            IEnumerable<MovieCommentViewModel> comments = this.movieCommentService.GetCommentsForGivenMovie(id);

            string country = this.countryService.GetCountryNameById(movie.CountryId);

            IQueryable<MovieListingViewModel> upNextMovies = this.dbContext.Movies
                .Where(m => m.Id != movie.Id)
                .Take(8)
                .Select(m => new MovieListingViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Duration = m.Duration,
                    ImageUrl = m.Images.First().Url,
                    Genres = m.MovieGenres.Select(m => m.Genre.Name),
                    StarRating = m.MovieUserStarRatings.Count != 0 ? m.MovieUserStarRatings.Sum(r => r.Rate) / m.MovieUserStarRatings.Count : -1,
                });

            return new MovieDetailsViewModel()
            {
                Id = movie.Id,
                Genres = genres,
                Images = images,
                Actors = actors,
                Name = movie.Name,
                Countruy = country,
                Director = director,
                Comments = comments,
                Platforms = platforms,
                Languages = languages,
                Duration = movie.Duration,
                UpNextMovies = upNextMovies,
                Trailer = movie.TrailerPath,
                Resolution = movie.Resolution,
                Description = movie.Description,
                DateOfReleased = movie.DateOfReleased,
                ApplicationUsersId = applicationUsersId,
            };
        }

        public async Task<MovieFormModel> GetMovieById(int id)
        {
            Movie movie = await this.dbContext.Movies.FindAsync();

            return new MovieFormModel()
            {
                Name = movie.Name,
            };
        }

        public AllMoviesQueryModel All
            (string searchName = null,
            MovieSorting sorting = MovieSorting.Rating,
            int currentPage = 1,
            int moviesPerPage = AllMoviesQueryModel.MoviesPerPage,
            string searchGenre = "all")
        {
            IQueryable<Movie> moviesQuery = this.dbContext.Movies.AsQueryable();

            if (searchGenre.ToLower() != "all")
            {
                IQueryable<Movie> moviesByTargetGenre = this.dbContext
                    .MovieGenres
                    .Where(m => m.Genre.Name.ToLower() == searchGenre.ToLower() && m.Movie.MovieGenres.Count == 1)
                    .Select(m => m.Movie);

                moviesQuery = moviesByTargetGenre;
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                moviesQuery = moviesQuery.Where(m => m.Name.ToLower().Contains(searchName));
            }

            // Default sorting is by rating
            moviesQuery = sorting switch
            {
                MovieSorting.Rating => moviesQuery
                    .OrderByDescending(m => m.MovieUserStarRatings.Count != 0 ? m.MovieUserStarRatings.Sum(r => r.Rate) / m.MovieUserStarRatings.Count : 0),
                MovieSorting.YearNewest => moviesQuery.OrderByDescending(m => m.DateOfReleased),
                MovieSorting.YearOldest => moviesQuery.OrderBy(m => m.DateOfReleased),
                MovieSorting.NameAlphabetically => moviesQuery.OrderBy(m => m.Name),
                MovieSorting.DurationSmallest => moviesQuery.OrderBy(m => m.Duration),
                MovieSorting.DurationLargest => moviesQuery.OrderByDescending(m => m.Duration),
                _ => moviesQuery.OrderByDescending(m => m.DateOfReleased),
            };

            IQueryable<MovieListingViewModel> movies = moviesQuery
                .Skip((currentPage - 1) * moviesPerPage)
                .Take(moviesPerPage)
                .Select(m => new MovieListingViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    ImageUrl = m.Images.First().Url,
                    StarRating = m.MovieUserStarRatings.Count != 0 ? m.MovieUserStarRatings.Sum(r => r.Rate) / m.MovieUserStarRatings.Count : -1,
                    Duration = m.Duration,
                    Genres = m.MovieGenres.Select(m => m.Genre.Name),
                });

            return new AllMoviesQueryModel()
            {
                Movies = movies,
                TotalMovies = moviesQuery.Count(),
                CurrentPage = currentPage,
            };
        }

        public IEnumerable<MovieHomeViewModel> GetTopThreeRatedMovies()
        {
            IEnumerable<MovieHomeViewModel> topThreeRatedMovies = this.dbContext
                .Movies
                .OrderByDescending(m => m.MovieUserStarRatings.Count != 0 ? m.MovieUserStarRatings.Sum(s => s.Rate) / m.MovieUserStarRatings.Count : 0)
                .Select(m => new MovieHomeViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    ImageUrl = m.Images.Select(i => i.Url).First(),
                })
                .Take(3);

            return topThreeRatedMovies;
        }

        public IEnumerable<UserSavedMovieViewModel> GetUserSavedMovies(string userId)
            => this.dbContext.ApplicationUserMovies.Where(a => a.UserId == userId).Select(a => a.Movie).Select(a => new UserSavedMovieViewModel()
            {
                UserId = userId,
                MovieId = a.Id,
                Name = a.Name,
                ImageUrl = a.Images.Select(i => i.Url).First(),
                Genres = a.MovieGenres.Select(m => m.Genre.Name),
                Duration = a.Duration,
                StarRating = a.MovieUserStarRatings.Count != 0 ? a.MovieUserStarRatings.Sum(m => m.Rate) / a.MovieUserStarRatings.Count : 0,
            });

        // Update
        public async Task<bool> EditAsync(EditMovieServiceModel movieModel)
        {
            try
            {
                Movie movie = this.dbContext
                .Movies
                .FirstOrDefault(m => m.Id == movieModel.MovieId);

                MovieFormModel newMovieData = movieModel.OverallMovieInformation;

                movie.Name = newMovieData.Name;
                movie.DateOfReleased = newMovieData.DateOfReleased;
                movie.Resolution = newMovieData.Resolution;
                movie.Description = newMovieData.Description;
                movie.TrailerPath = newMovieData.TrailerPath;
                movie.Duration = newMovieData.Duration;

                int directorId = this.directorService.GetDirectorIdByGivenFullName(newMovieData.Director.FullName);
                if (directorId == 0)
                {
                    await this.directorService.CreateAsync(newMovieData.Director);
                    directorId = this.directorService.GetDirectorIdByGivenFullName(newMovieData.Director.FullName);
                }
                else if (directorId != 0)
                {
                    await this.directorService.EditDirectorAsync(directorId, newMovieData.Director);
                }

                movie.DirectorId = directorId;

                // When edit movie country create new country
                Country country = this.countryService.GetCountryByName(newMovieData.Country);
                if (country == null)
                {
                    await this.countryService.CreateAsync(newMovieData.Country);
                    int countryId = this.countryService.GetCountryIdByGivenName(newMovieData.Country);
                    movie.CountryId = countryId;
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException();
            }

            return true;
        }

        // Delete
        public async Task<bool> DeleteAsync(DeleteMovieServiceModel deleteMovieServiceModel)
        {
            int movieId = deleteMovieServiceModel.Id;

            try
            {
                IEnumerable<MovieLanguage> movieLanguages = this.dbContext.MovieLanguages.Where(m => m.MovieId == movieId);
                foreach (MovieLanguage movieLanguage in movieLanguages)
                {
                    movieLanguage.IsDeleted = true;
                    movieLanguage.DeletedOn = DateTime.UtcNow;
                }

                IEnumerable<MovieActor> movieActors = this.dbContext.MovieActors.Where(m => m.MovieId == movieId);
                foreach (MovieActor movieActor in movieActors)
                {
                    movieActor.IsDeleted = true;
                    movieActor.DeletedOn = DateTime.UtcNow;
                }

                IEnumerable<MovieGenre> movieGenres = this.dbContext.MovieGenres.Where(m => m.MovieId == movieId);
                foreach (MovieGenre movieGenre in movieGenres)
                {
                    movieGenre.IsDeleted = true;
                    movieGenre.DeletedOn = DateTime.UtcNow;
                }

                IEnumerable<MoviePlatform> moviePlatforms = this.dbContext.MoviePlatform.Where(m => m.MovieId == movieId);
                foreach (MoviePlatform moviePlatform in moviePlatforms)
                {
                    moviePlatform.IsDeleted = true;
                    moviePlatform.DeletedOn = DateTime.UtcNow;
                }

                IEnumerable<Image> images = this.dbContext.Images.Where(m => m.MovieId == movieId);
                foreach (Image image in images)
                {
                    image.IsDeleted = true;
                }

                Movie movie = this.dbContext.Movies.FirstOrDefault(m => m.Id == movieId);
                movie.IsDeleted = true;
                movie.DeletedOn = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public bool CheckIfMovieWithGivenIdExist(int id)
            => this.dbContext.Movies.Any(m => m.Id == id);

        // Useful methods
        private async Task MatchLanguagesWithMovie(int movieId, IEnumerable<int> languagesIds)
        {
            ICollection<MovieLanguage> movieLanguages = new HashSet<MovieLanguage>();

            foreach (int languageId in languagesIds)
            {
                movieLanguages.Add(new MovieLanguage() { MovieId = movieId, LanguageId = languageId });
            }

            await this.dbContext.MovieLanguages.AddRangeAsync(movieLanguages);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task MatchGenresWithMovie(int movieId, IEnumerable<int> genresIds)
        {
            ICollection<MovieGenre> movieGenres = new HashSet<MovieGenre>();

            foreach (int genreId in genresIds)
            {
                movieGenres.Add(new MovieGenre() { MovieId = movieId, GenreId = genreId });
            }

            await this.dbContext.MovieGenres.AddRangeAsync(movieGenres);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task MatchActorsWithMovie(int movieId, ICollection<int> actorsIds)
        {
            ICollection<MovieActor> movieActors = new HashSet<MovieActor>();

            foreach (int actorId in actorsIds)
            {
                movieActors.Add(new MovieActor() { MovieId = movieId, ActorId = actorId });
            }

            await this.dbContext.MovieActors.AddRangeAsync(movieActors);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task MatchPlatformsWithMovie(int movieId, ICollection<int> platformsIds)
        {
            ICollection<MoviePlatform> moviePlatforms = new HashSet<MoviePlatform>();

            foreach (int platformId in platformsIds)
            {
                moviePlatforms.Add(new MoviePlatform() { MovieId = movieId, PlatformId = platformId });
            }

            await this.dbContext.AddRangeAsync(moviePlatforms);
            await this.dbContext.SaveChangesAsync();
        }

        private string GetUserId(ClaimsPrincipal user)
        {
            return this.userManager.GetUserId(user);
        }
    }
}
