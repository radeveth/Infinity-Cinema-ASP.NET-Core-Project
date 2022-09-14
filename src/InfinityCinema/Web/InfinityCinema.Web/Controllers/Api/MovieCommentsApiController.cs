namespace InfinityCinema.Web.Controllers.Api
{
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.MovieCommentsService;
    using InfinityCinema.Services.Data.MovieCommentsService.Models;
    using InfinityCinema.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/moviecomments/")]
    public class MovieCommentsApiController : BaseController
    {
        private readonly IMovieCommentService movieCommentService;

        public MovieCommentsApiController(IMovieCommentService movieCommentService)
        {
            this.movieCommentService = movieCommentService;
        }

        [HttpGet]
        [Route("vote")]
        public async Task<ActionResult<MovieCommentVotesResponseModel>> Vote(int commentId, bool isLikeVote)
        {
            string userId = ClaimsPrincipalExtensions.GetId(this.User);

            MovieCommentVotesResponseModel response = await this.movieCommentService.Vote(commentId, userId, isLikeVote);
            return response;
        }
    }
}
