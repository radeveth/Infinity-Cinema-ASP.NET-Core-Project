namespace InfinityCinema.Services.Data.ImagesService
{
    using System.Threading.Tasks;

    public interface IImageService
    {
        Task<string> CreateAsync(ImageFormModel imageFormModel);
    }
}
