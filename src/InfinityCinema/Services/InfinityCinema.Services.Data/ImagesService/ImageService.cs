namespace InfinityCinema.Services.Data.ImagesService
{
    using System;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;

    public class ImageService : IImageService
    {
        private readonly InfinityCinemaDbContext dbConext;

        public ImageService(InfinityCinemaDbContext dbConext)
        {
            this.dbConext = dbConext;
        }

        public async Task<Image> CreateAsync(ImageFormModel imageFormModel)
        {
            Image image = new Image()
            {
                Url = imageFormModel.ImageUrl,
                MovieId = imageFormModel.MovieId,
            };

            await this.dbConext.AddAsync(image);
            await this.dbConext.SaveChangesAsync();

            return image;
        }
    }
}
