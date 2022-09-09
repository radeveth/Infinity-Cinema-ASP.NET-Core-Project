namespace InfinityCinema.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ForumSystem.CategoriesService;
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models;
    using InfinityCinema.Services.Data.ForumSystem.PostsService;
    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;
    using InfinityCinema.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ForumCategoriesController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IPostService postService;

        public ForumCategoriesController(ICategoryService categoryService, IPostService postService)
        {
            this.categoryService = categoryService;
            this.postService = postService;
        }

        [HttpGet]
        public IActionResult GetCategory(int id)
        {
            CategoryViewModel category = this.categoryService.GetViewModelById<CategoryViewModel>(id);

            return this.View(category);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreatePostForCategory([FromQuery] int categoryId)
        {
            this.ViewData["CategoryTitle"] = this.categoryService.GetViewModelById<CategoryViewModel>(categoryId).Title;

            return this.View(new PostFormModel()
            {
                CategoryId = categoryId,
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePostForCategoryAsync(PostFormModel postFormModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(postFormModel);
            }

            postFormModel.UserId = ClaimsPrincipalExtensions.GetId(this.User);

            await this.postService.CreateAsync<PostViewModel>(postFormModel);

            return this.RedirectToAction(nameof(this.GetCategory), "ForumCategories", postFormModel.CategoryId);
        }
    }
}
