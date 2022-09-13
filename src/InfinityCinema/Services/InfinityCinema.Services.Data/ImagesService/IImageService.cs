namespace InfinityCinema.Services.Data.ImagesService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ImagesService.Models;

    public interface IImageService
    {
        // Create
        Task<T> CreateAsync<T>(ImageFormModel imageFormModel);

        // Read
        IEnumerable<string> GetImagesForGivenMovie(int movieId);

        T GetViewModelById<T>(int id);

        IEnumerable<T> GetViewModelByMovieId<T>(int movieId);

        // Update

        // Delete
        Task DeleteImagesForParticularMovie(int movieId);

        Task DeleteAsync(int id);
    }
}
