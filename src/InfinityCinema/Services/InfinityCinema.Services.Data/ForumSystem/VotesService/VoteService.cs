namespace InfinityCinema.Services.Data.ForumSystem.VotesService
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models.Enums;
    using InfinityCinema.Data.Models.ForumSystem;
    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;
    using InfinityCinema.Services.Data.ForumSystem.VotesService.Models;
    using InfinityCinema.Services.Mapping;

    public class VoteService : IVoteService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public VoteService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create
        public async Task<T> CreateAsync<T>(VoteFormModel voteFormModel)
        {
            Vote vote = new Vote()
            {
                PostId = voteFormModel.PostId,
                UserId = voteFormModel.UserId,
            };

            await this.dbContext.Votes.AddAsync(vote);
            await this.dbContext.SaveChangesAsync();

            return this.GetViewModelById<T>(vote.Id);
        }

        public async Task VoteAsync(VoteFormModel voteFormModel)
        {
            Vote vote = this.dbContext
                .Votes.FirstOrDefault(v => v.PostId == voteFormModel.PostId && v.UserId == voteFormModel.UserId);

            if (vote != null)
            {
                vote = await this.CreateAsync<Vote>(new VoteFormModel()
                {
                    PostId = voteFormModel.PostId,
                    UserId = voteFormModel.UserId,
                });
            }

            vote.Type = voteFormModel.IsLikeVote ? VoteType.Like : VoteType.Dislike;
            await this.dbContext.SaveChangesAsync();
        }

        // Read
        public T GetViewModelById<T>(int id)
            => this.dbContext
                .Votes
                .Where(v => v.Id == id)
                .To<T>()
                .FirstOrDefault();

        public int GetVotes(int postId)
            => this.dbContext
                .Votes
                .Where(v => v.PostId == postId)
                .Sum(V => (int)V.Type);

        public PostVotesViewModel GetVotesForGivenPost(int postId)
        {
            IEnumerable<Vote> votes = this.dbContext.Votes.Where(v => v.PostId == postId);

            return new PostVotesViewModel()
            {
                Likes = votes.Where(v => v.Type == VoteType.Like).Count(),
                Dislikes = votes.Where(v => v.Type == VoteType.Dislike).Count(),
            };
        }
    }
}
