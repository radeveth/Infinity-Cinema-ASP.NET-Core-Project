namespace InfinityCinema.Services.Data.ForumSystem.PostsService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Common.Repositories;
    using InfinityCinema.Data.Models.ForumSystem;
    using InfinityCinema.Services.Data.ForumSystem.CommentsService;
    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;
    using InfinityCinema.Services.Data.ForumSystem.VotesService;
    using InfinityCinema.Services.Mapping;

    public class PostService : IPostService
    {
        private readonly InfinityCinemaDbContext dbContext;
        private readonly ICommentService commentService;
        private readonly IVoteService voteService;

        public PostService(InfinityCinemaDbContext dbContext, ICommentService commentService, IVoteService voteService)
        {
            this.dbContext = dbContext;
            this.commentService = commentService;
            this.voteService = voteService;
        }

        // Create
        public async Task<T> CreateAsync<T>(PostFormModel postFormModel)
        {
            Post post = new Post()
            {
                Title = postFormModel.Title,
                Content = postFormModel.Content,
                UserId = postFormModel.UserId,
                CategoryId = postFormModel.CategoryId,
            };

            await this.dbContext.Posts.AddAsync(post);
            await this.dbContext.SaveChangesAsync();

            return this.GetViewModelById<T>(post.Id);
        }

        // Read
        public T GetViewModelById<T>(int id)
            => this.dbContext
                .Posts
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefault();

        public bool IfPostExist(int postId)
            => this.dbContext.Posts.Any(p => p.Id == postId);

        // Delete
        public async Task DeleteAsync(int postId)
        {
            Post post = this.dbContext.Posts.Where(p => p.Id == postId).AsQueryable().FirstOrDefault();

            if (post != null)
            {
                foreach (var comment in this.dbContext.Comments.Where(c => c.PostId == post.Id).ToList())
                {
                    await this.commentService.DeleteAsync(comment.Id);
                }

                foreach (var vote in this.dbContext.Votes.Where(v => v.PostId == post.Id).ToList())
                {
                    await this.voteService.DeleteAsync(vote.Id);
                }

                post.IsDeleted = true;
                post.DeletedOn = DateTime.UtcNow;
                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}
