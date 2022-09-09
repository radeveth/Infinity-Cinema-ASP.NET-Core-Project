namespace InfinityCinema.Services.Data.ForumSystem.PostsService
{
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ForumSystem.PostsService.Models;

    public interface IPostService
    {
        Task<T> CreateAsync<T>(PostFormModel postFormModel);

        T GetViewModelById<T>(int id);
    }
}
