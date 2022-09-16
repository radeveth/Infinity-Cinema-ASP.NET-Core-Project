namespace InfinityCinema.Services.Data.ApplicationUserMoviesService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using InfinityCinema.Services.Data.ApplicationUserMoviesService.Models;

    public interface IApplicationUserMovieService
    {
        Task<bool> SaveMovieToWatchLaterAsync(int movieId, string userId);

        Task<bool> RemoveMovieFromWatchLaterAsync(int movieId, string userId);

        public IEnumerable<string> GetUsersIdsThatAreSaveGivenMovie(int movieId);

        IEnumerable<UserSavedMovieViewModel> GetUserSavedMovies(string userId);
    }
}
