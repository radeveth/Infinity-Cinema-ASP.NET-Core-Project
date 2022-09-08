namespace InfinityCinema.Services.Data.ImagesService.Models
{
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Mapping;

    public class ImageViewModel : IMapFrom<Image>
    {
        public int Id { get; set; }

        public string Url { get; set; }
    }
}
