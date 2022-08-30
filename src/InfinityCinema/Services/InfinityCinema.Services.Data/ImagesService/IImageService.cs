namespace InfinityCinema.Services.Data.ImagesService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Services.Data.ImagesService.Models;

    public interface IImageService
    {
        // Create
        Task<ImageViewModel> CreateAsync(ImageFormModel imageFormModel);

        // Read
        IEnumerable<string> GetImagesForGivenMovie(int movieId);

        // Update

        // Delete
        Task DeleteImagesForParticularMovie(int movieId);
    }
}
