namespace InfinityCinema.Web.Controllers
{
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService;
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models;
    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ForumCategoriesController : BaseController
    {
        private readonly ICategoryService categoryService;

        public ForumCategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategoryByTitle([FromQuery] string title)
        {
            CategoryViewModel category = this.categoryService.GetCategoryByTitle<CategoryViewModel>(title);

            return this.View(category);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreatePostForCategoryAsync([FromQuery] int categoryId)
        {
            return null;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreatePostForCategoryAsync(PostFormModel postFormModel)
        {
            return null;
        }
    }
}
