namespace InfinityCinema.Services.Data.ForumSystem.CategoriesService
{
    using System.Collections.Generic;

    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models;
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models.Enums;

    public interface ICategoryService
    {
        // Read
        IEnumerable<T> GetAll<T>(CategorySorting categorySorting);

        T GetCategoryByTitle<T>(string title);
    }
}
