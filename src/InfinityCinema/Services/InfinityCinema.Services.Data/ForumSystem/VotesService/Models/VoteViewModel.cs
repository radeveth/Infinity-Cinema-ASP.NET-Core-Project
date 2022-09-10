namespace InfinityCinema.Services.Data.ForumSystem.VotesService.Models
{
    using InfinityCinema.Data.Models.Enums;
    using InfinityCinema.Data.Models.ForumSystem;
    using InfinityCinema.Services.Mapping;

    public class VoteViewModel : IMapFrom<Vote>
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string UserId { get; set; }

        public VoteType Type { get; set; }
    }
}
