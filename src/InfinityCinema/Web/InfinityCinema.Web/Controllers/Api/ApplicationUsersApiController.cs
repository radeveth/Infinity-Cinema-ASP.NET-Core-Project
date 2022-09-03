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

        [HttpGet]
        [Route("ratemovie")]
        public async Task<ActionResult<string>> RateMovie(int id, decimal rating)
        {
            if (rating < 0 || rating > 10)
            {
                return "Invalid rating is given";
            }

            string userId = ClaimsPrincipalExtensions.GetId(this.User);

            if (!this.movieService.CheckIfMovieWithGivenIdExist(id))
            {
                return this.RedirectToAction("Details", "Movies", id);
            }

            if (this.applicationUserService.CheckIfUserIsAlreadyRatedThisMovie(id, userId))
            {
                return "You are already rated this movie";
            }

            await this.applicationUserService.RateMovie(id, userId, rating);

            return $"Successfully rate this movie with {rating}";
        }
    }
}
