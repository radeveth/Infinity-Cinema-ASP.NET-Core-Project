namespace InfinityCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Services.Data.MoviesService.Models;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : BaseController
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;

        public MoviesController(IMovieService movieService, IGenreService genreService)
        {
            this.movieService = movieService;
            this.genreService = genreService;
        }

        public IActionResult Create()
        {
            return this.View(new CreateMovieServiceModel() { OverallMovieInformation = CreateInitializateOfinitialization(new MovieFormModel(), this.genreService) });
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
                    { OverallMovieInformation = CreateInitializateOfinitialization(new MovieFormModel(), this.genreService) });
            }

            string message = await this.movieService.CreateMovieAsync(movieModel, this.User);
            if (message != null)
            {
                return this.RedirectToAction(nameof(Index), "Home");
            }
            else
            {
                return this.View(new CreateMovieServiceModel()
                    { OverallMovieInformation = CreateInitializateOfinitialization(new MovieFormModel(), this.genreService) });
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
            return this.View(new CreateMovieServiceModel() { OverallMovieInformation = CreateInitializateOfinitialization(new MovieFormModel(), this.genreService) });
        }

        private static MovieFormModel CreateInitializateOfinitialization(MovieFormModel movieFormModel, IGenreService genreService)
            => new MovieFormModel()
                {
                    Genres = genreService.GetMovieGenres(),
                };
    }
}
