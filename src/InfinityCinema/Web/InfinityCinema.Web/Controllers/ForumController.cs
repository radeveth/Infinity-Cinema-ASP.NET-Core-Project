namespace InfinityCinema.Web.Controllers
{
    using System.Collections.Generic;

    using InfinityCinema.Services.Data.ForumSystem.CategoriesService;
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models;
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models.Enums;
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
    }
}
