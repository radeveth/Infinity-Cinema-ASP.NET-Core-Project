namespace InfinityCinema.Services.Data.ForumSystem.VotesService.Models
{
    public class VoteFormModel
    {
        public int PostId { get; set; }

        public string UserId { get; set; }

        public bool IsLikeVote { get; set; }
    }
}
