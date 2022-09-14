namespace InfinityCinema.Web.Controllers.Api
{
    using System.Threading.Tasks;
    using InfinityCinema.Services.Data.ForumSystem.PostsService;
    using InfinityCinema.Services.Data.ForumSystem.VotesService;
    using InfinityCinema.Services.Data.ForumSystem.VotesService.Models;
    using InfinityCinema.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/forumvotesapi/")]
    public class ForumVotesApiController : ControllerBase
    {
        private readonly IVoteService voteService;
        private readonly IPostService postService;

        public ForumVotesApiController(IVoteService voteService, IPostService postService)
        {
            this.voteService = voteService;
            this.postService = postService;
        }

        [HttpGet]
        [Authorize]
        [Route("vote")]
        public async Task<ActionResult<VotesResponseModel>> Vote(int postId, bool isLikeVote)
        {
            if (!this.postService.IfPostExist(postId))
            {
                return this.BadRequest();
            }

            if (isLikeVote != true && isLikeVote != false)
            {
                return this.BadRequest();
            }

            VoteFormModel voteFormModel = new VoteFormModel()
            {
                PostId = postId,
                UserId = ClaimsPrincipalExtensions.GetId(this.User),
                IsLikeVote = isLikeVote,
            };

            await this.voteService.VoteAsync(voteFormModel);

            VotesResponseModel response = this.voteService.GetVotesForGivenPost(postId);
            return response;
        }
    }
}
