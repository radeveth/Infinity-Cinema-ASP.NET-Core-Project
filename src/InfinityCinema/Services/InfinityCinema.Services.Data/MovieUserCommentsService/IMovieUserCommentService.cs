namespace InfinityCinema.Services.Data.MovieUserCommentsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMovieUserCommentService
    {
        // Create
        Task CreateAsync(int movieId, int commentId);

        // Read
        public IEnumerable<T> GetCommentsForGivenMovie<T>(int movieId);

        public IEnumerable<string> GetUsersIdsThatAreCommentInGivenMovie(int movieId);
    }
}
