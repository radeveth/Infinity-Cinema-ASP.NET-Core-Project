namespace InfinityCinema.Web.Controllers
{
    using System.Collections.Generic;

    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Services.Data.MoviesService.Models;
    using InfinityCinema.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    public class ApplicationUsersController : BaseController
    {
        private readonly IMovieService movieService;

        public ApplicationUsersController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public IActionResult WatchList()
        {
            string userId = ClaimsPrincipalExtensions.GetId(this.User);
            IEnumerable<UserSavedMovieViewModel> userSavedMovieViewModels = this.movieService.GetUserSavedMovies(userId);

            return this.View(userSavedMovieViewModels);
        }
    }
}
