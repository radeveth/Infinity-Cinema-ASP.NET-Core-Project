namespace InfinityCinema.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

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
            return this.View(new CreateMovieServiceModel() { OverallMovieInformation = CreateInitializateOfinitialization(new MovieFormModel(), this.genreService) });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateMovieServiceModel movieModel)
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
            return this.View();
        }

        private static MovieFormModel CreateInitializateOfinitialization(MovieFormModel movieFormModel, IGenreService genreService)
            => new MovieFormModel()
                {
                    Genres = genreService.GetMovieGenres(),
                };
    }
}
