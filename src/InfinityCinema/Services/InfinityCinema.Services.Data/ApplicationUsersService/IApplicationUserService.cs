namespace InfinityCinema.Services.Data.ApplicationUsersService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IApplicationUserService
    {
        Task<bool> SaveMovieToWatchLaterAsync(int movieId, string userId);

        Task<bool> RemoveMovieFromWatchLaterAsync(int movieId, string userId);

        Task RateMovie(int movieId, string userId, decimal rating);

        T GetViewModelById<T>(string id);

        IEnumerable<string> GetUsersIdsThatAreCommentInGivenMovie(int movieId);

        IEnumerable<string> GetUsersIdsThatAreSaveGivenMovie(int movieId);

        bool CheckIfUserIsAlreadyRatedThisMovie(int movieId, string userId);
    }
}
