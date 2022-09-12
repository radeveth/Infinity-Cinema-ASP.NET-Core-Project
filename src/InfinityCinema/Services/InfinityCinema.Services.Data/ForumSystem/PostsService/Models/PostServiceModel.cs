namespace InfinityCinema.Services.Data.ForumSystem.PostsService.Models
{
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Enums;
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Models;

    public class PostServiceModel
    {
        public PostViewModel Post { get; set; }

        public CommentFormModel Comment { get; set; }

        public CommentSorting CommentsSorting { get; set; }
    }
}
