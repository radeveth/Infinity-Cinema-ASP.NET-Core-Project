namespace InfinityCinema.Services.Data.MovieCommentsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.MovieCommentsService.Models;

    public interface IMovieCommentService
    {
        // Cread
        Task<MovieCommentViewModel> CreateAsync(MovieCommentFormModel comment);

        // Read
        IEnumerable<MovieCommentViewModel> GetCommentsForGivenMovie(int movieId);

        // Update
        // Delete
    }
}
