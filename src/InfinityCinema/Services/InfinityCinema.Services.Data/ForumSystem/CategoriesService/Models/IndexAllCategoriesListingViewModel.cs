namespace InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models
{
    using System.Collections.Generic;

    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models.Enums;

    public class IndexAllCategoriesListingViewModel
    {
        public CategorySorting Sorting { get; set; }

        public IEnumerable<IndexCategoryViewModel> Categories { get; set; }
    }
}
