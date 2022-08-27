namespace InfinityCinema.Services.Data.ImagesService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ImagesService.Models;

    public class ImageService : IImageService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public ImageService(InfinityCinemaDbContext dbConext)
        {
            this.dbContext = dbConext;
        }

        // Create
        public async Task<ImageViewModel> CreateAsync(ImageFormModel imageFormModel)
        {
            Image image = new Image()
            {
                Url = imageFormModel.ImageUrl,
                MovieId = imageFormModel.MovieId,
            };

            await this.dbContext.AddAsync(image);
            await this.dbContext.SaveChangesAsync();

            return new ImageViewModel()
            {
                Id = image.Id,
                Url = image.Url,
            };
        }

        // Read
        public IEnumerable<string> GetImagesForGivenMovie(int movieId)
            => this.dbContext
                .Images
                .Where(i => i.MovieId == movieId).Select(i => i.Url);

        // Update

        // Delete
        public async Task DeleteImagesForParticularMovie(int movieId)
        {
            IQueryable<Image> images = this.dbContext.Images.Where(i => i.MovieId == movieId);

            this.dbContext.RemoveRange(images);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
