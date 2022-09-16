namespace InfinityCinema.Services.Data.ApplicationUserMoviesService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ApplicationUserMoviesService.Models;
    using InfinityCinema.Services.Mapping;

    public class ApplicationUserMovieService : IApplicationUserMovieService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public ApplicationUserMovieService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> SaveMovieToWatchLaterAsync(int movieId, string userId)
        {
            ApplicationUser user = this.dbContext.Users.FirstOrDefault(u => u.Id == userId);
            Movie movie = this.dbContext.Movies.FirstOrDefault(m => m.Id == movieId);

            this.dbContext.ApplicationUserMovies.Add(new ApplicationUserMovie()
            {
                UserId = user.Id,
                MovieId = movie.Id,
            });

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveMovieFromWatchLaterAsync(int movieId, string userId)
        {
            ApplicationUser user = this.dbContext.Users.FirstOrDefault(u => u.Id == userId);
            Movie movie = this.dbContext.Movies.FirstOrDefault(m => m.Id == movieId);

            ApplicationUserMovie applicationUserMovie = this.dbContext
                .ApplicationUserMovies
                .FirstOrDefault(a => a.UserId == user.Id && a.MovieId == movie.Id);

            this.dbContext.ApplicationUserMovies.Remove(applicationUserMovie);

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public IEnumerable<string> GetUsersIdsThatAreSaveGivenMovie(int movieId)
        {
            return (IEnumerable<string>)this.dbContext
                .ApplicationUserMovies
                .Where(m => m.MovieId == movieId)
                .Distinct()
                .Select(m => m.UserId);
        }

        public IEnumerable<UserSavedMovieViewModel> GetUserSavedMovies(string userId)
            => this.dbContext
                .ApplicationUserMovies
                .Where(a => a.UserId == userId)
                .Select(a => a.Movie)
                .To<UserSavedMovieViewModel>();
    }
}
