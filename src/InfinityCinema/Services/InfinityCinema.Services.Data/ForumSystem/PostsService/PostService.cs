namespace InfinityCinema.Services.Data.ForumSystem.PostsService
{
    using System;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Common.Repositories;
    using InfinityCinema.Data.Models.ForumSystem;
    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;

    public class PostService : IPostService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;

        public PostService(IDeletableEntityRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task<T> CreateAsync<T>(PostFormModel postFormModel)
        {
            await this.postRepository.AddAsync(new Post()
            {
                Title = postFormModel.Title,
                Content = postFormModel.Title,
                UserId = postFormModel.UserId,
                CategoryId = postFormModel.CategoryId,
            });

            throw new NotImplementedException();
        }
    }
}
