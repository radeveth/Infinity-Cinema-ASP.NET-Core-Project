namespace InfinityCinema.Services.Data.ImagesService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ImagesService.Models;

    public interface IImageService
    {
        Task<Image> CreateAsync(ImageFormModel imageFormModel);
    }
}
