namespace InfinityCinema.Web.Controllers
{
    using InfinityCinema.Services.Data.GenresService;
    using InfinityCinema.Services.Data.MoviesService;
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
            var movieFormModel = new MovieFormModel()
            {
                Genres = this.genreService.GetMovieGenres(),
            };

            return this.View(new CreateMovieServiceModel() { OverallMovieInformation = movieFormModel });
        }

        [HttpPost]
        public IActionResult Create(CreateMovieServiceModel movieModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(movieModel);
            }

            // string result = this.movieService.CreateMovie(movieModel);

            // return this.Json(result);
            return null;
        }
    }
}
