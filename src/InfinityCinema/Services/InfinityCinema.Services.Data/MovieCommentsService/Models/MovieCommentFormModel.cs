namespace InfinityCinema.Services.Data.MovieCommentsService.Models
{
    public class MovieCommentFormModel
    {
        public string Content { get; set; }

        public int MovieId { get; set; }

        public string UserId { get; set; }
    }
}
