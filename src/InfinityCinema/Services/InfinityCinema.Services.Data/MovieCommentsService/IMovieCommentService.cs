namespace InfinityCinema.Services.Data.MovieCommentsService
{
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.MovieCommentsService.Models;

    public interface IMovieCommentService
    {
        // Cread
        Task<T> CreateAsync<T>(MovieCommentFormModel comment);

        // Read
        T GetViewModelById<T>(int id);

        // Update
        Task<MovieCommentVotesResponseModel> Vote(int commentId, string userId, bool isLikeVote);

        // Delete
    }
}
