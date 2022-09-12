namespace InfinityCinema.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using InfinityCinema.Services.Data.ForumSystem.CommentsService;
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Models;
    using InfinityCinema.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

    public class ForumCommentsController : BaseController
    {
        private readonly ICommentService commentService;

        public ForumCommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public IActionResult AllForParticularPost(int postId)
        {
            return this.View(this.commentService
                .GetAllForParticluarPost<CommentViewModel>(postId));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(CommentFormModel comment)
        {
            comment.UserId = ClaimsPrincipalExtensions.GetId(this.User);
            await this.commentService.CreateAsync<CommentViewModel>(comment);

            return this.RedirectToAction("GetPost", "ForumPosts", new { postId = comment.PostId });
        }
    }
}
