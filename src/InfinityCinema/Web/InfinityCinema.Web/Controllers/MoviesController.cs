namespace InfinityCinema.Web.Controllers
{
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.MoviesService;
    using Microsoft.AspNetCore.Mvc;
    using System;

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
        public IActionResult Create(CreateMovieServiceModel movieModel)
        {
            var isGenreExist = this.genreService.IsGenresExists(movieModel.OverallMovieInformation.GenresId);

            if (!isGenreExist)
            {
                this.ModelState.AddModelError(string.Empty, "One of given genre does not exist");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(new CreateMovieServiceModel()
                    { OverallMovieInformation = CreateInitializateOfinitialization(new MovieFormModel(), this.genreService) });
            }

            // string result = this.movieService.CreateMovie(movieModel);

            // return this.Json(result);
            return null;
        }

        private static MovieFormModel CreateInitializateOfinitialization(MovieFormModel movieFormModel, IGenreService genreService)
            => new MovieFormModel()
                {
                    Genres = genreService.GetMovieGenres(),
                };
    }
}
