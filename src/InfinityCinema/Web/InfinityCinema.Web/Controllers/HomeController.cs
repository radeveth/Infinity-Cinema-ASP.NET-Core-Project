namespace InfinityCinema.Web.Controllers
{
    using System.Diagnostics;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.HomeService.Models;
    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Web.Infrastructure;
    using InfinityCinema.Web.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IMovieService movieService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public HomeController(IMovieService movieService, SignInManager<ApplicationUser> signInManager)
        {
            this.movieService = movieService;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            IndexViewModel indexViewModel;
            if (this.signInManager.IsSignedIn(this.User))
            {
                string userId = ClaimsPrincipalExtensions.GetId(this.User);

                indexViewModel = new IndexViewModel()
                {
                    TopThreeRatedMovies = this.movieService.GetTopThreeRatedMovies(),
                    SavdMoviesFromUser = this.movieService.GetUserSavedMovies(userId),
                };

                this.ViewBag.IsUserIsSignedIn = true;
            }
            else
            {
                indexViewModel = new IndexViewModel()
                {
                    TopThreeRatedMovies = this.movieService.GetTopThreeRatedMovies(),
                };

                this.ViewBag.IsUserIsSignedIn = false;
            }

            return this.View(indexViewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
