namespace InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models.Enums;

    public class IndexAllCategoriesListingViewModel
    {
        [Display(Name = "")]
        public CategorySorting Sorting { get; set; } = 0;

        public IEnumerable<IndexCategoryViewModel> Categories { get; set; }
    }
}
