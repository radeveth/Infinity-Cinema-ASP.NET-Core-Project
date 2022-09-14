namespace InfinityCinema.Services.Data.ForumSystem.CommentsService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models.ForumSystem;
    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Models;
    using InfinityCinema.Services.Mapping;

    public class CommentService : ICommentService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public CommentService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create
        public async Task<T> CreateAsync<T>(CommentFormModel commentFormModel)
        {
            Comment comment = new Comment()
            {
                Content = commentFormModel.Content,
                PostId = commentFormModel.PostId,
                ParentId = commentFormModel.ParentId,
                UserId = commentFormModel.UserId,
            };

            await this.dbContext.Comments.AddAsync(comment);
            await this.dbContext.SaveChangesAsync();

            return this.GetViewModelById<T>(comment.Id);
        }

        // Read
        public T GetViewModelById<T>(int id)
            => this.dbContext
                .Comments
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();

        public IEnumerable<T> GetAllForParticluarPost<T>(int postId)
            => this.dbContext
                .Comments
                .Where(c => c.PostId == postId)
                .To<T>();

        // Delete
        public async Task DeleteAsync(int commentId)
        {
            Comment comment = await this.dbContext.Comments.FindAsync(commentId);

            if (comment != null)
            {
                List<Comment> childComments = this.dbContext.Comments.Where(c => c.ParentId == comment.Id).ToList();

                if (childComments.Any())
                {
                    foreach (var childComment in childComments)
                    {
                        await this.DeleteAsync(childComment.Id);
                    }
                }

                this.dbContext.Comments.Remove(comment);
                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}
