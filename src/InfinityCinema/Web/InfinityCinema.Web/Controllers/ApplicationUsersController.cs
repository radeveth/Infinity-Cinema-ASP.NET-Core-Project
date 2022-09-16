namespace InfinityCinema.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using InfinityCinema.Services.Data.ApplicationUserMoviesService;
    using InfinityCinema.Services.Data.ApplicationUserMoviesService.Models;
    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

    public class ApplicationUsersController : BaseController
    {
        private readonly IApplicationUserMovieService applicationUserMovieService;

        public ApplicationUsersController(IApplicationUserMovieService applicationUserMovieService)
        {
            this.applicationUserMovieService = applicationUserMovieService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult WatchList()
        {
            string userId = ClaimsPrincipalExtensions.GetId(this.User);
            IEnumerable<UserSavedMovieViewModel> userSavedMovieViewModels = this.applicationUserMovieService.GetUserSavedMovies(userId);

            return this.View(userSavedMovieViewModels);
        }
    }
}
