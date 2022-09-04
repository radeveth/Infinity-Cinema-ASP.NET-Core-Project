namespace InfinityCinema.Services.Data.MovieCommentsService.Models
{
    using InfinityCinema.Services.Data.ApplicationUsersService.Models;

    public class MovieCommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int MovieId { get; set; }

        public ApplicationUserViewModel User { get; set; }
    }
}
