namespace InfinityCinema.Services.Data.MovieUserStarRatingsService
{
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;

    public class MovieUserStarRatingService : IMovieUserStarRatingService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public MovieUserStarRatingService(InfinityCinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
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

        public bool CheckIfUserIsAlreadyRatedThisMovie(int movieId, string userId)
            => this.dbContext.MovieUserStarRatings.Any(m => m.UserId == userId && m.MovieId == movieId);
    }
}
