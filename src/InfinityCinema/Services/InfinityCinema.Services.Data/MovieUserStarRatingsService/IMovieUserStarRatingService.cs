namespace InfinityCinema.Services.Data.MovieUserStarRatingsService
{
    using System.Threading.Tasks;

    public interface IMovieUserStarRatingService
    {
        Task RateMovie(int movieId, string userId, decimal rating);

        bool CheckIfUserIsAlreadyRatedThisMovie(int movieId, string userId);
    }
}
