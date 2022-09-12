namespace InfinityCinema.Services.Data.ForumSystem.CommentsService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ForumSystem.CommentsService.Models;

    public interface ICommentService
    {
        // Create
        Task<T> CreateAsync<T>(CommentFormModel commentFormModel);

        // Read
        T GetViewModelById<T>(int id);

        IEnumerable<T> GetAllForParticluarPost<T>(int postId);

        // Update

        // Delete
        Task DeleteAsync(int commentId);
    }
}
