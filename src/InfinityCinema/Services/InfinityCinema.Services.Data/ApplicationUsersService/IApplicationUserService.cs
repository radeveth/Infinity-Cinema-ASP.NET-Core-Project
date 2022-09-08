namespace InfinityCinema.Services.Data.ApplicationUsersService
{
    using InfinityCinema.Services.Data.ApplicationUsersService.Models;

    using System.Threading.Tasks;

    public interface IApplicationUserService
    {
        Task<bool> SaveMovieToWatchLaterAsync(int movieId, string userId);

        Task<bool> RemoveMovieFromWatchLaterAsync(int movieId, string userId);

        Task RateMovie(int movieId, string userId, decimal rating);

        T GetViewModelById<T>(string id);

        bool CheckIfUserIsAlreadyRatedThisMovie(int movieId, string userId);
    }
}
