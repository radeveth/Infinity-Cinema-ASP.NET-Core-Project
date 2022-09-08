namespace InfinityCinema.Services.Data.ApplicationUsersService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ApplicationUsersService.Models;
    using InfinityCinema.Services.Mapping;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public ApplicationUserService(InfinityCinemaDbContext dbContext)
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

        public async Task RateMovie(int movieId, string userId, decimal rating)
        {
            this.dbContext.MovieUserStarRatings.Add(new MovieUserStarRating()
            {
                MovieId = movieId,
                UserId = userId,
                Rate = rating,
            });

            await this.dbContext.SaveChangesAsync();
        }

        public T GetViewModelById<T>(string id)
            => this.dbContext
                .Users
                .Where(u => u.Id == id)
                .To<T>()
                .FirstOrDefault();

        public IEnumerable<string> GetUsersIdsThatAreCommentInGivenMovie(int movieId)
        {
            IQueryable<MovieUserComment> movieUserComments = this.dbContext.MovieUserComments.Where(m => m.MovieId == movieId).Distinct();
            IQueryable<MovieComment> movieComments = movieUserComments.Select(m => m.MovieComment);
            return (IEnumerable<string>)movieComments.Select(m => m.UserId);
        }

        public IEnumerable<string> GetUsersIdsThatAreSaveGivenMovie(int movieId)
        {
            return (IEnumerable<string>)this.dbContext
                .ApplicationUserMovies
                .Where(m => m.MovieId == movieId)
                .Distinct()
                .Select(m => m.UserId);
        }

        public bool CheckIfUserIsAlreadyRatedThisMovie(int movieId, string userId)
            => this.dbContext.MovieUserStarRatings.Any(m => m.UserId == userId && m.MovieId == movieId);
    }
}
