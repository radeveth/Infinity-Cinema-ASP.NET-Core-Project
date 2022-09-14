namespace InfinityCinema.Services.Data.MovieCommentsService.Models
{
    using System.Linq;

    using AutoMapper;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Data.Models.Enums;
    using InfinityCinema.Services.Data.ApplicationUsersService.Models;
    using InfinityCinema.Services.Mapping;

    public class MovieCommentViewModel : IMapFrom<MovieComment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public ApplicationUserViewModel User { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MovieComment, MovieCommentViewModel>()
                .ForMember(x => x.Likes, y => y.MapFrom(s => s.MovieCommentVotes.Where(x => x.Vote == VoteType.Like).Count()))
                .ForMember(x => x.Dislikes, y => y.MapFrom(s => s.MovieCommentVotes.Where(x => x.Vote == VoteType.Dislike).Count()));
        }
    }
}
