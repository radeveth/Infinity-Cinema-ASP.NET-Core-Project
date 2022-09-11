namespace InfinityCinema.Services.Data.ForumSystem.PostsService
{
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;

    public interface IPostService
    {
        // Create
        Task<T> CreateAsync<T>(PostFormModel postFormModel);

        // Read
        T GetViewModelById<T>(int id);

        bool IfPostExist(int postId);
    }
}
