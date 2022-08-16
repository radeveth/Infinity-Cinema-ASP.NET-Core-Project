namespace InfinityCinema.Web.Controllers
{
    using InfinityCinema.Services.Data.MoviesService;
    using Microsoft.AspNetCore.Mvc;

    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public IActionResult Create()
        {
            return this.View(new MovieFormModel());
        }

        [HttpPost]
        public IActionResult Create(MovieFormModel movieModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(movieModel);
            }

            string result = this.movieService.CreateMovie(movieModel);

            return this.Json(result);
        }
    }
}
