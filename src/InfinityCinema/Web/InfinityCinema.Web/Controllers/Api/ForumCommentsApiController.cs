namespace InfinityCinema.Web.Controllers.Api
{
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ForumSystem.CommentsService;
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/forumcommentsapi/")]
    public class ForumCommentsApiController : BaseController
    {
        private readonly ICommentService commentService;

        public ForumCommentsApiController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [Route("delete")]
        public async Task<ActionResult<string>> DeleteAsync([FromQuery] int commentId)
        {
            if (this.commentService.GetViewModelById<CommentViewModel>(commentId) == null)
            {
                return this.BadRequest("Something wrong!!! Invalid operation");
            }

            await this.commentService.DeleteAsync(commentId);

            return this.Ok("Comment was successfully deleted.");
        }
    }
}
