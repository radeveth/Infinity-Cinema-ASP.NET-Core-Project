namespace InfinityCinema.Web.Areas.Administration.Controllers
{
    using InfinityCinema.Services.Data.MoviesService;

    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class DashboardController : AdministrationController
    {
        private readonly MovieService movieService;

        public DashboardController(MovieService movieService)
        {
            this.movieService = movieService;
        }

        public IActionResult Index()
        {
            return this.View(this.movieService.MovieStatistics());
        }
    }
}
