namespace InfinityCinema.Web.Controllers
{
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ForumSystem.CommentsService;
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Models;
    using InfinityCinema.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ForumPostsController : Controller
    {
        private readonly ICommentService commentService;

        public ForumPostsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult ReplayWithCommentOnCategoryPost(int categoryId, int postId)
        {
            return this.View(new CommentFormModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ReplayWithCommentOnCategoryPostAsync(CommentFormModel commentFormModel, int postId)
        {
            commentFormModel.PostId = postId;
            commentFormModel.UserId = ClaimsPrincipalExtensions.GetId(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(commentFormModel);
            }

            await this.commentService.CreateAsync<CommentFormModel>(commentFormModel);

            return this.RedirectToAction("AllForParticularPost", "ForumComments");
        }
    }
}
