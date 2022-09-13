namespace InfinityCinema.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Common;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.ActorsService.Models;
    using InfinityCinema.Services.Data.DirectorsService.Models;
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.GenresService.Models;
    using InfinityCinema.Services.Data.ImagesService;
    using InfinityCinema.Services.Data.ImagesService.Models;
    using InfinityCinema.Services.Data.MovieCommentsService;
    using InfinityCinema.Services.Data.MovieCommentsService.Models;
    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Services.Data.MoviesService.Models;
    using InfinityCinema.Services.Data.MovieUserCommentsService;
    using InfinityCinema.Services.Data.PlatformsService;
    using InfinityCinema.Services.Data.PlatformsService.Models;
    using InfinityCinema.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : BaseController
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        private readonly IActorService actorService;
        private readonly IImageService imagesService;
        private readonly IPlatformService platformService;
        private readonly IMovieCommentService movieCommentService;
        private readonly IMovieUserCommentService movieUserCommentService;

        public MoviesController(IMovieService movieService, IGenreService genreService, IActorService actorService, IImageService imagesService, IPlatformService platformService, IMovieCommentService movieCommentService, IMovieUserCommentService movieUserCommentService)
        {
            this.movieService = movieService;
            this.genreService = genreService;
            this.actorService = actorService;
            this.imagesService = imagesService;
            this.platformService = platformService;
            this.movieCommentService = movieCommentService;
            this.movieUserCommentService = movieUserCommentService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            return this.View(new CreateMovieServiceModel()
            {
                OverallMovieInformation = CreateInitializationOfMovieGenres(new MovieFormModel(), this.genreService),
            });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> CreateAsync(CreateMovieServiceModel movieModel)
        {
            IEnumerable<int> genresIds = movieModel.OverallMovieInformation.Genres.Select(g => g.Id);

            if (!this.genreService.IsGenresExists(genresIds))
            {
                this.ModelState.AddModelError(string.Empty, "One of given genre does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(new CreateMovieServiceModel()
                { OverallMovieInformation = CreateInitializationOfMovieGenres(new MovieFormModel(), this.genreService) });
            }

            await this.movieService.CreateMovieAsync(movieModel, this.User);

            return this.RedirectToAction(nameof(this.All), "Movies");
        }

        [HttpGet]
        public IActionResult All([FromQuery] AllMoviesQueryModel moviesQueryModel)
        {
            AllMoviesQueryModel queryResult = this.movieService
                .All(moviesQueryModel.SearchName, moviesQueryModel.Sorting, moviesQueryModel.CurrentPage, AllMoviesQueryModel.MoviesPerPage, moviesQueryModel.SearchGenre);

            moviesQueryModel.Movies = queryResult.Movies;
            moviesQueryModel.TotalMovies = queryResult.TotalMovies;
            moviesQueryModel.CurrentPage = queryResult.CurrentPage;
            moviesQueryModel.SearchGenre = queryResult.SearchGenre;

            return this.View(moviesQueryModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            MovieDetailsServiceModel movie = this.movieService.Details(id);

            return this.View(movie);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DetailsAsync(int id, string newCommentContent)
        {
            string userId = ClaimsPrincipalExtensions.GetId(this.User);

            if (string.IsNullOrEmpty(newCommentContent))
            {
                this.ModelState.AddModelError(string.Empty, "Invalid comment!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(newCommentContent);
            }

            if (!string.IsNullOrEmpty(newCommentContent))
            {
                MovieCommentViewModel commentViewModel = await this.movieCommentService.CreateAsync<MovieCommentViewModel>(new MovieCommentFormModel()
                {
                    Content = newCommentContent,
                    UserId = userId,
                });

                await this.movieUserCommentService.CreateAsync(id, commentViewModel.Id);
            }

            return this.RedirectToAction(nameof(this.Details), "Movies");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditIndex(int id)
        {
            this.ViewBag.MovieId = id;

            return this.View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditMainInformation(int id)
        {
            this.ViewBag.MovieId = id;

            return this.View(this.movieService.GetMovieFormModel(id));
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditMainInformationAsync(MovieFormModel movie, int id)
        {
            if (movie.GenresId != null)
            {
                IEnumerable<int> genresIds = movie.GenresId;

                if (!this.genreService.IsGenresExists(genresIds))
                {
                    this.ModelState.AddModelError(string.Empty, "One of given genre does not exist");
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(movie);
            }

            await this.movieService.EditAsync(movie, id);

            return this.RedirectToAction(nameof(this.EditIndex), "Movies", new { id = id });
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditMovieImages(EditImagesServiceModel imagesService)
        {
            this.ViewBag.MovieName = this.movieService.GetViewModelById<MovieListingViewModel>(imagesService.MovieId).Name;
            return this.View(new EditImagesServiceModel()
            {
                MovieId = imagesService.MovieId,
                ExistingImages = this.imagesService.GetViewModelByMovieId<ImageViewModel>(imagesService.MovieId),
                NewImage = new ImageFormModel(),
            });
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditMovieActors(EditActorsServiceModel actorsService)
        {
            this.ViewBag.MovieName = this.movieService.GetViewModelById<MovieListingViewModel>(actorsService.MovieId).Name;

            return this.View(new EditActorsServiceModel()
            {
                MovieId = actorsService.MovieId,
                ExistingActors = this.actorService.GetActorsForGivenMovie<ActorViewModel>(actorsService.MovieId),
                NewActor = new ActorFormModel(),
            });
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditMoviePlatforms(EditPlatformsServiceModel platformsService)
        {
            this.ViewBag.MovieName = this.movieService.GetViewModelById<MovieListingViewModel>(platformsService.MovieId).Name;

            return this.View(new EditPlatformsServiceModel()
            {
                MovieId = platformsService.MovieId,
                ExistingPlatforms = this.platformService.GetPlatformsForGivenMovie<PlatformViewModel>(platformsService.MovieId),
                NewPlatform = new PlatformFormModel(),
            });
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult DeleteConfirmation(int id)
        {
            DeleteMovieServiceModel deleteMovieServiceModel = new DeleteMovieServiceModel()
            {
                Id = id,
            };
            return this.View(deleteMovieServiceModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            DeleteMovieServiceModel deleteMovieServiceModel = new DeleteMovieServiceModel()
            {
                Id = id,
            };

            if (!this.movieService.CheckIfMovieWithGivenIdExist(id))
            {
                this.ModelState.AddModelError(string.Empty, "This movie does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(deleteMovieServiceModel);
            }

            await this.movieService.DeleteAsync(deleteMovieServiceModel);

            return this.RedirectToAction(nameof(this.All), "Movies");
        }

        private static MovieFormModel CreateInitializationOfMovieGenres(MovieFormModel movieFormModel, IGenreService genreService)
            => new MovieFormModel()
            {
                Genres = genreService.GetMovieGenres(),
            };
    }
}
