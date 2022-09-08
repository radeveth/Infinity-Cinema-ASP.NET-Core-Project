namespace InfinityCinema.Services.Data.ForumSystem.CategoriesService
{
    using System.Collections.Generic;
    using System.Linq;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Common.Repositories;
    using InfinityCinema.Data.Models.ForumSystem;
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models.Enums;
    using InfinityCinema.Services.Mapping;

    public class CategoryService : ICategoryService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository, InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Read
        public IEnumerable<T> GetAll<T>(CategorySorting categorySorting = CategorySorting.MostPopular)
        {
            IQueryable<Category> categoriesQuery = this.dbContext.Categories;

            // Default sort is by most popular
            categoriesQuery = categorySorting switch
            {
                CategorySorting.Ascending => categoriesQuery.OrderBy(c => c.Title),
                CategorySorting.Descending => categoriesQuery.OrderByDescending(c => c.Title),
                CategorySorting.Newest => categoriesQuery.OrderByDescending(c => c.Id),
                CategorySorting.Oldest => categoriesQuery.OrderBy(c => c.Id),
                CategorySorting.MostPopular => categoriesQuery.OrderByDescending(c => c.Posts.Count),
                CategorySorting.MostUnpopular => categoriesQuery.OrderBy(c => c.Posts.Count),
                _ => categoriesQuery.OrderByDescending(c => c.Posts.Count),
            };

            return categoriesQuery.To<T>().ToList();
        }

        public T GetCategoryByTitle<T>(string title)
        {
            T category = this.dbContext.Categories
                .Where(c => c.Title == title)
                .To<T>().FirstOrDefault();

            return category;
        }
    }
}
