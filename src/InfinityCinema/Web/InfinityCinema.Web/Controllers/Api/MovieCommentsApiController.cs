namespace InfinityCinema.Web.Controllers.Api
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
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

        [Route("create")]
        public async Task<ActionResult> Create(string content, int movieId)
        {
            MovieCommentFormModel movieComment = new MovieCommentFormModel()
            {
                Content = content,
                MovieId = movieId,
                UserId = ClaimsPrincipalExtensions.GetId(this.User),
            };

            await this.movieCommentService.CreateAsync(movieComment);

            IEnumerable<MovieCommentViewModel> comments = this.movieCommentService.GetCommentsForGivenMovie(movieId);

            return this.Json(comments);
        }
    }
}
