namespace InfinityCinema.Services.Data.MovieCommentsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.MovieCommentsService.Models;

    public interface IMovieCommentService
    {
        // Cread
        Task<T> CreateAsync<T>(MovieCommentFormModel comment);

        // Read
        T GetViewModelById<T>(int id);

        // Update
        Task<int> IncreaseCommentLikesAsync(int commentId);

        Task<int> IncreaseCommentDislikesAsync(int commentId);

        // Delete
    }
}
