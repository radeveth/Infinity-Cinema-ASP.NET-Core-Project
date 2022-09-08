namespace InfinityCinema.Web.Controllers
{
    using System.Collections.Generic;

    using InfinityCinema.Services.Data.ForumSystem.CategoriesService;
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models;
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models.Enums;
    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ForumController : BaseController
    {
        private readonly ICategoryService categoryService;

        public ForumController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] CategorySorting sorting)
        {
            IEnumerable<IndexCategoryViewModel> categories = this.categoryService.GetAll<IndexCategoryViewModel>(sorting);
            IndexAllCategoriesListingViewModel listingViewModel = new IndexAllCategoriesListingViewModel()
            {
                Categories = categories,
                Sorting = sorting,
            };
            return this.View(listingViewModel);
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
