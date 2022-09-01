namespace InfinityCinema.Web.Controllers.Api
{
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ApplicationUsersService;
    using InfinityCinema.Services.Data.MoviesService;
    using InfinityCinema.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/applicationusersapi")]
    [ApiController]
    public class ApplicationUsersApiController : ControllerBase
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly IMovieService movieService;

        public ApplicationUsersApiController(IApplicationUserService applicationUserService, IMovieService movieService)
        {
            this.applicationUserService = applicationUserService;
            this.movieService = movieService;
        }

        [HttpGet]
        [Route("savetowatchlater")]
        public async Task<ActionResult<string>> SaveToWatchLaterAsync(int id)
        {
            string userId = ClaimsPrincipalExtensions.GetId(this.User);

            if (!this.movieService.CheckIfMovieWithGivenIdExist(id))
            {
                return this.RedirectToAction("Details", "Movies", id);
            }

            //if (this.applicationUserService.IfUserIsSavedThisMovie(id, userId))
            //{
            //    return "Movie is already saved to latch later!";
            //}

            await this.applicationUserService.SaveMovieToWatchLaterAsync(id, userId);

            return "Successfully saved to watch later!";
        }

        [HttpGet]
        [Route("removefromwatchlater")]
        public async Task<ActionResult<string>> RemoveFromWatchLaterAsync(int id)
        {
            string userId = ClaimsPrincipalExtensions.GetId(this.User);

            if (!this.movieService.CheckIfMovieWithGivenIdExist(id))
            {
                return this.RedirectToAction("Details", "Movies", id);
            }

            await this.applicationUserService.RemoveMovieFromWatchLaterAsync(id, userId);

            return "Removed from watch later!";
        }
    }
}
