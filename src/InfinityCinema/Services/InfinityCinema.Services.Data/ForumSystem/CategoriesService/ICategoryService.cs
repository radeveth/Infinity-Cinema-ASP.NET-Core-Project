namespace InfinityCinema.Services.Data.ForumSystem.CategoriesService
{
    using System.Collections.Generic;

    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models;
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models.Enums;

    public interface ICategoryService
    {
        // Read
        IEnumerable<T> GetAll<T>(CategorySorting categorySorting);

        CategoryServiceModel ViewCategory(int categoryId, int currentPage = 1, CategorySorting sorting = CategorySorting.Newest, int postPerPage = CategoryServiceModel.PostsPerPage);

        T GetCategoryByTitle<T>(string title);

        T GetViewModelById<T>(int id);
    }
}
