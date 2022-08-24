namespace InfinityCinema.Services.Data.ImagesService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ImagesService.Models;

    public interface IImageService
    {
        Task<Image> CreateAsync(ImageFormModel imageFormModel);

        IEnumerable<string> GetImagesForGivenMovie(int movieId);

        Task DeleteImagesForParticularMovie(int movieId);
    }
}
