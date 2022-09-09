namespace InfinityCinema.Services.Data.ForumSystem.PostsService
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models.ForumSystem;
    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;
    using InfinityCinema.Services.Mapping;

    public class PostService : IPostService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public PostService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> CreateAsync<T>(PostFormModel postFormModel)
        {
            Post post = new Post()
            {
                Title = postFormModel.Title,
                Content = postFormModel.Title,
                UserId = postFormModel.UserId,
                CategoryId = postFormModel.CategoryId,
            };

            await this.dbContext.Posts.AddAsync(post);
            await this.dbContext.SaveChangesAsync();

            return this.GetViewModelById<T>(post.Id);
        }

        public T GetViewModelById<T>(int id)
            => this.dbContext
                .Posts
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefault();
    }
}
