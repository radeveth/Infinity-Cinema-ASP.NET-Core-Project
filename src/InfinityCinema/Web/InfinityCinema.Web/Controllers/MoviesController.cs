namespace InfinityCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.ActorsService.Models;
    using InfinityCinema.Services.Data.DirectorsService.Models;
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.GenresService.Models;
    using InfinityCinema.Services.Data.ImagesService;
    using InfinityCinema.Services.Data.ImagesService.Models;
    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Services.Data.MoviesService.Models;
    using InfinityCinema.Services.Data.PlatformsService;
    using InfinityCinema.Services.Data.PlatformsService.Models;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : BaseController
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        private readonly IActorService actorService;
        private readonly IImageService imagesService;
        private readonly IPlatformService platformService;

        public MoviesController(IMovieService movieService, IGenreService genreService, IActorService actorService, IImageService imagesService, IPlatformService platformService)
        {
            this.movieService = movieService;
            this.genreService = genreService;
            this.actorService = actorService;
            this.imagesService = imagesService;
            this.platformService = platformService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View(new CreateMovieServiceModel()
            {
                OverallMovieInformation = CreateInitializationOfMovieGenres(new MovieFormModel(), this.genreService),
            });
        }

        [HttpPost]
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

            string message = await this.movieService.CreateMovieAsync(movieModel, this.User);
            if (message != null)
            {
                return this.RedirectToAction(nameof(Index), "Home");
            }
            else
            {
                return this.View(new CreateMovieServiceModel()
                { OverallMovieInformation = CreateInitializationOfMovieGenres(new MovieFormModel(), this.genreService) });
            }
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
            MovieDetailsViewModel movie = this.movieService.Details(id);

            return this.View(movie);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            MovieDetailsViewModel targetMovie = this.movieService.Details(id);

            MovieFormModel movieFormModel = new MovieFormModel()
            {
                Name = targetMovie.Name,
                Genres = targetMovie.Genres.Select(g => new GenreFormModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    ImageUrl = g.ImageUrl,
                }),
                Description = targetMovie.Description,
                Director = new DirectorFormModel()
                {
                    FullName = targetMovie.Director.FullName,
                    InformationUrl = targetMovie.Director.InformationLink,
                },
                TrailerPath = targetMovie.Trailer,
                DateOfReleased = targetMovie.DateOfReleased,
                Resolution = targetMovie.Resolution,
                Duration = targetMovie.Duration,
                Language = string.Join(", ", targetMovie.Languages),
                Country = targetMovie.Countruy,
            };

            EditMovieServiceModel editMovieServiceModel = new EditMovieServiceModel()
            {
                OverallMovieInformation = movieFormModel,
                Actors = new EditActorsFormModel()
                {
                    ExistingActors = targetMovie.Actors,
                },
                Images = new EditImagesFormModel()
                {
                    ExistingImages = targetMovie.Images,
                },
                Platforms = new EditPlatformsFormModel()
                {
                    ExistingPlatforms = targetMovie.Platforms,
                },
            };
            return this.View(editMovieServiceModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(EditMovieServiceModel movieModel, int id)
        {
            if (movieModel.OverallMovieInformation.GenresId != null)
            {
                IEnumerable<int> genresIds = movieModel.OverallMovieInformation.GenresId;

                if (!this.genreService.IsGenresExists(genresIds))
                {
                    this.ModelState.AddModelError(string.Empty, "One of given genre does not exist");
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(movieModel);
            }

            movieModel.MovieId = id;
            bool isSuccessfullEditing = await this.movieService.EditAsync(movieModel);

            if (!isSuccessfullEditing)
            {
                return this.View(movieModel);
            }

            return this.RedirectToAction(nameof(this.All), "Movies");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await this.movieService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All), "Movies");
        }

        private static MovieFormModel CreateInitializationOfMovieGenres(MovieFormModel movieFormModel, IGenreService genreService)
            => new MovieFormModel()
            {
                Genres = genreService.GetMovieGenres(),
            };
    }
}
