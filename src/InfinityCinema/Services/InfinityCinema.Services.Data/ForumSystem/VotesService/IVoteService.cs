namespace InfinityCinema.Services.Data.ForumSystem.VotesService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models.Enums;
    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;
    using InfinityCinema.Services.Data.ForumSystem.VotesService.Models;

    public interface IVoteService
    {
        // Create
        Task<T> CreateAsync<T>(VoteFormModel voteFormModel);

        Task VoteAsync(VoteFormModel voteFormModel);

        // Read
        T GetViewModelById<T>(int id);

        int GetVotes(int postId);

        PostVotesViewModel GetVotesForGivenPost(int postId);

        // Update
        // Delete
    }
}
