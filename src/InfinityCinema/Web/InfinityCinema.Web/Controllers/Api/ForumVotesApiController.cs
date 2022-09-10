namespace InfinityCinema.Web.Controllers.Api
{
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;
    using InfinityCinema.Services.Data.ForumSystem.VotesService;
    using InfinityCinema.Services.Data.ForumSystem.VotesService.Models;
    using InfinityCinema.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/[controller]")]
    public class ForumVotesApiController : ControllerBase
    {
        private readonly IVoteService voteService;

        public ForumVotesApiController(IVoteService voteService)
        {
            this.voteService = voteService;
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<ActionResult<PostVotesViewModel>> Vote(VoteFormModel voteFormModel, int postId)
        {
            voteFormModel.UserId = ClaimsPrincipalExtensions.GetId(this.User);
            await this.voteService.VoteAsync(voteFormModel);

            return this.voteService.GetVotesForGivenPost(postId);
        }
    }
}
