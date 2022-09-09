namespace InfinityCinema.Services.Data.ForumSystem.CommentsService.Models
{
    using InfinityCinema.Data.Models.ForumSystem;
    using InfinityCinema.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public ApplicationUserInCommentViewModel User { get; set; }
    }
}
