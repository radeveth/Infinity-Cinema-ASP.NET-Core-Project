namespace InfinityCinema.Services.Data.ForumSystem.CategoriesService.Models
{
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Models;

    public class CategoryServiceModel
    {
        public CategoryViewModel Category { get; set; }

        public CommentFormModel Comment { get; set; }
    }
}
