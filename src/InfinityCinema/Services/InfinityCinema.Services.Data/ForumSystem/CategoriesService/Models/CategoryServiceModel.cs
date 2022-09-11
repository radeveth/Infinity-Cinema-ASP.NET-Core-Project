namespace InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models
{
    using InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models.Enums;
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Models;

    public class CategoryServiceModel
    {
        public const int PostsPerPage = 10;

        public int CategoryId { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalPostsForCategory { get; set; }

        public CategorySorting PostsSorting { get; set; } = CategorySorting.Newest;

        public CategoryViewModel Category { get; set; }

        public CommentFormModel Comment { get; set; }
    }
}
