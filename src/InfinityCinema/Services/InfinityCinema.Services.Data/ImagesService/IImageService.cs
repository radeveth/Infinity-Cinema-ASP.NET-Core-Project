namespace InfinityCinema.Services.Data.ImagesService
{
    using System.Threading.Tasks;

    using InfinityCinema.Data.Models;

    public interface IImageService
    {
        Task<Image> CreateAsync(ImageFormModel imageFormModel);
    }
}
