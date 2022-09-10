namespace InfinityCinema.Web.Controllers
{
    using InfinityCinema.Services.Data.ForumSystem.CommentsService;
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Models;
    using Microsoft.AspNetCore.Mvc;

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
    }
}
