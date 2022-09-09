namespace InfinityCinema.Web.Controllers.Api
{
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.MovieCommentsService;
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
        [Route("increaselikes")]
        public async Task<ActionResult<int>> IncreaseLikes(int commentId)
        {
            return await this.movieCommentService.IncreaseCommentLikesAsync(commentId);
        }

        [HttpGet]
        [Route("increasedislikes")]
        public async Task<ActionResult<int>> IncreaseDislikes(int commentId)
        {
            return await this.movieCommentService.IncreaseCommentDislikesAsync(commentId);
        }
    }
}
