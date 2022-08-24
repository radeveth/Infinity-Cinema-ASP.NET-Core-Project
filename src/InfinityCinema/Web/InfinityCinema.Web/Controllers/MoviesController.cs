namespace InfinityCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.ActorsService.Models;
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.ImagesService;
    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Services.Data.MoviesService.Models;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : BaseController
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        private readonly IActorService actorService;
        private readonly IImageService imagesService;

        public MoviesController(IMovieService movieService, IGenreService genreService, IActorService actorService, IImageService imagesService)
        {
            this.movieService = movieService;
            this.genreService = genreService;
            this.actorService = actorService;
            this.imagesService = imagesService;
        }

        public IActionResult Create()
        {
            return this.View(new CreateMovieServiceModel() { OverallMovieInformation = CreateInitializationOfMovieGenres(new MovieFormModel(), this.genreService) });
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

        public IActionResult All([FromQuery] AllMoviesQueryModel moviesQueryModel)
        {
            AllMoviesQueryModel queryResult = this.movieService
                .All(moviesQueryModel.SearchName, moviesQueryModel.Sorting, moviesQueryModel.CurrentPage, AllMoviesQueryModel.MoviesPerPage, moviesQueryModel.SearchGenre);

            moviesQueryModel.TotalMovies = queryResult.TotalMovies;
            moviesQueryModel.Movies = queryResult.Movies;
            moviesQueryModel.CurrentPage = queryResult.CurrentPage;
            moviesQueryModel.SearchGenre = queryResult.SearchGenre;

            return this.View(moviesQueryModel);
        }

        public IActionResult Details(int id)
        {
            MovieDetailsViewModel movie = this.movieService.Details(id);

            return this.View(movie);
        }

        public IActionResult Edit(int id)
        {
            IEnumerable<ActorViewModel> existingActors = this.actorService.GetActorsForGivenMovie(id);
            IEnumerable<string> existingImages = this.imagesService.GetImagesForGivenMovie(id);

            return this.View(new EditMovieServiceModel() { OverallMovieInformation = CreateInitializationOfMovieGenres(new MovieFormModel(), this.genreService) });
        }

        [HttpPost]
        public IActionResult Edit([FromQuery] EditMovieServiceModel)
        {
            return this.View(new EditMovieServiceModel() { OverallMovieInformation = CreateInitializationOfMovieGenres(new MovieFormModel(), this.genreService) });
        }

        private static MovieFormModel CreateInitializationOfMovieGenres(MovieFormModel movieFormModel, IGenreService genreService)
            => new MovieFormModel()
                {
                    Genres = genreService.GetMovieGenres(),
                };
    }
}
