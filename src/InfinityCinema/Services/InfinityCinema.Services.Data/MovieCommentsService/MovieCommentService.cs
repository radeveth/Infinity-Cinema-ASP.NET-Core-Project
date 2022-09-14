namespace InfinityCinema.Services.Data.MovieCommentsService
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Data.Models.Enums;
    using InfinityCinema.Services.Data.ApplicationUsersService;
    using InfinityCinema.Services.Data.ForumSystem.VotesService.Models;
    using InfinityCinema.Services.Data.MovieCommentsService.Models;
    using InfinityCinema.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class MovieCommentService : IMovieCommentService
    {
        private readonly InfinityCinemaDbContext dbContext;
        private readonly IApplicationUserService userService;

        public MovieCommentService(InfinityCinemaDbContext dbContext, IApplicationUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        // Create
        public async Task<T> CreateAsync<T>(MovieCommentFormModel comment)
        {
            MovieComment movieComment = new MovieComment()
            {
                Content = comment.Content,
                UserId = comment.UserId,
            };

            await this.dbContext.MovieComments.AddAsync(movieComment);
            await this.dbContext.SaveChangesAsync();

            return this.GetViewModelById<T>(movieComment.Id);
        }

        // Read
        public T GetViewModelById<T>(int id)
            => this.dbContext
                .MovieComments
                .Where(m => m.Id == id)
                .To<T>()
                .FirstOrDefault();

        // Update
        public async Task<MovieCommentVotesResponseModel> Vote(int commentId, string userId, bool isLikeVote)
        {
            MovieCommentVote movieCommentVote = this.dbContext.MovieCommentVotes.Where(m => m.MovieCommentId == commentId && m.UserId == userId).FirstOrDefault();

            if (movieCommentVote == null)
            {
                movieCommentVote = new MovieCommentVote();
                movieCommentVote = new MovieCommentVote()
                {
                    MovieCommentId = commentId,
                    UserId = userId,
                };
                await this.dbContext.MovieCommentVotes.AddAsync(movieCommentVote);
                await this.dbContext.SaveChangesAsync();
            }

            movieCommentVote.Vote = isLikeVote == true ? VoteType.Like : VoteType.Dislike;
            await this.dbContext.SaveChangesAsync();

            MovieCommentViewModel comment = this.GetViewModelById<MovieCommentViewModel>(commentId);
            return new MovieCommentVotesResponseModel()
            {
                Likes = comment.Likes,
                Dislikes = comment.Dislikes,
            };
        }
    }
}
