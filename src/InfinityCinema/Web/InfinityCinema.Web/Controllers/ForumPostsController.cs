namespace InfinityCinema.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ForumSystem.CommentsService;
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Enums;
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Models;
    using InfinityCinema.Services.Data.ForumSystem.PostsService;
    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;
    using InfinityCinema.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ForumPostsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IPostService postService;

        public ForumPostsController(ICommentService commentService, IPostService postService)
        {
            this.commentService = commentService;
            this.postService = postService;
        }

        [HttpGet]
        public IActionResult GetPost(int postId, CommentSorting commentsSorting)
        {
            PostViewModel post = this.postService.GetViewModelById<PostViewModel>(postId);
            if (commentsSorting == CommentSorting.Oldest)
            {
                post.Comments = post.Comments.OrderByDescending(c => c.Id).ToList();
            }

            return this.View(new PostServiceModel()
            {
                Post = post,
                Comment = new CommentFormModel(),
            });
        }

        [HttpGet]
        [Authorize]
        public IActionResult ReplayWithCommentOnCategoryPost(int postId)
        {
            return this.View(new CommentFormModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ReplayWithCommentOnCategoryPostAsync(CommentFormModel commentFormModel, int postId)
        {
            commentFormModel.PostId = postId;
            commentFormModel.UserId = ClaimsPrincipalExtensions.GetId(this.User);

            //if (!this.ModelState.IsValid)
            //{
            //    return this.View(commentFormModel);
            //}

            await this.commentService.CreateAsync<CommentFormModel>(commentFormModel);

            return this.RedirectToAction("AllForParticularPost", "ForumComments");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeletePostAsync(int postId, int categoryId)
        {
            await this.postService.DeleteAsync(postId);

            return this.RedirectToAction("GetCategory", "ForumCategories", new { categoryId = categoryId });
        }
    }
}
