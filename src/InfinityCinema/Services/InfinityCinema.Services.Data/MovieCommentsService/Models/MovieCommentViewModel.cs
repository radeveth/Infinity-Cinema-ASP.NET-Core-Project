namespace InfinityCinema.Services.Data.MovieCommentsService.Models
{
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ApplicationUsersService.Models;
    using InfinityCinema.Services.Mapping;

    public class MovieCommentViewModel : IMapFrom<MovieComment>
    {
        public int Id { get; set; }

        public ApplicationUserViewModel User { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
