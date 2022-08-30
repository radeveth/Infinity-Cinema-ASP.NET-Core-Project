namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Services.Data.MoviesService.Models;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly IMovieService movieService;

        public DashboardController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public IActionResult Statistics()
        {
            MovieStatisticsViewModel movieStatistics = this.movieService.MovieStatistics();

            return this.View(movieStatistics);
        }
    }
}
