namespace InfinityCinema.Services.Data.MovieUserCommentsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.MovieCommentsService.Models;

    public interface IMovieUserCommentService
    {
        // Create
        Task CreateAsync(int movieId, int commentId);

        // Read
        public IEnumerable<MovieCommentViewModel> GetCommentsForGivenMovie(int movieId);
    }
}
