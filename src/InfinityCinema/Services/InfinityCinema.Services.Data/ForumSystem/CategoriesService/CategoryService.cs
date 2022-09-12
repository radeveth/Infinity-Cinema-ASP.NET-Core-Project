namespace InfinityCinema.Services.Data.ForumSystem.CategoriesService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.ConstrainedExecution;
    using InfinityCinema.Data;
    using InfinityCinema.Data.Common.Repositories;
    using InfinityCinema.Data.Models.ForumSystem;
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models;
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models.Enums;
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Models;
    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;
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

        public CategoryServiceModel ViewCategory(int categoryId, int currentPage = 1, CategorySorting sorting = CategorySorting.Newest, int postsPerPage = CategoryServiceModel.PostsPerPage)
        {
            CategoryViewModel category = this.GetViewModelById<CategoryViewModel>(categoryId);
            int totalPostsForCategory = category.Posts.Count();

            category.Posts = sorting switch
            {
                CategorySorting.Ascending => category.Posts.OrderBy(c => c.Title),
                CategorySorting.Descending => category.Posts.OrderByDescending(c => c.Title),
                CategorySorting.Newest => category.Posts.OrderByDescending(p => p.CreatedOn),
                CategorySorting.Oldest => category.Posts.OrderBy(p => p.CreatedOn),
                CategorySorting.MostPopular => category.Posts.OrderByDescending(c => c.Likes),
                CategorySorting.MostUnpopular => category.Posts.OrderBy(c => c.Dislikes),
                _ => category.Posts.OrderByDescending(c => c.Id),
            };

            List<PostViewModel> targetPosts = category.Posts
                .Skip((currentPage - 1) * postsPerPage)
                .Take(postsPerPage)
                .ToList();
            category.Posts = targetPosts;

            return new CategoryServiceModel()
            {
                CategoryId = categoryId,
                Category = category,
                CurrentPage = currentPage,
                TotalPostsForCategory = totalPostsForCategory,
            };
        }

        public T GetCategoryByTitle<T>(string title)
        {
            T category = this.dbContext.Categories
                .Where(c => c.Title == title)
                .To<T>().FirstOrDefault();

            return category;
        }

        public T GetViewModelById<T>(int id)
            => this.dbContext
                .Categories
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();
    }
}
