namespace InfinityCinema.Services.Data.ImagesService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ImagesService.Models;
    using InfinityCinema.Services.Mapping;

    public class ImageService : IImageService
    {
        private readonly InfinityCinemaDbContext dbContext;

        public ImageService(InfinityCinemaDbContext dbConext)
        {
            this.dbContext = dbConext;
        }

        // Create
        public async Task<T> CreateAsync<T>(ImageFormModel imageFormModel)
        {
            Image image = new Image()
            {
                Url = imageFormModel.ImageUrl,
                MovieId = imageFormModel.MovieId,
            };

            await this.dbContext.AddAsync(image);
            await this.dbContext.SaveChangesAsync();

            return this.GetViewModelById<T>(image.Id);
        }

        // Read
        public IEnumerable<string> GetImagesForGivenMovie(int movieId)
        {
            IEnumerable<string> images = this.dbContext
                .Images
                .Where(i => i.MovieId == movieId)
                .Select(i => i.Url);

            if (images.Any())
            {
                return images;
            }

            return null;
        }

        public T GetViewModelById<T>(int id)
            => this.dbContext
                .Images
                .Where(i => i.Id == id)
                .To<T>()
                .FirstOrDefault();

        public IEnumerable<T> GetViewModelByMovieId<T>(int movieId)
        {
            IEnumerable<T> images = this.dbContext
                .Images
                .Where(i => i.MovieId == movieId)
                .To<T>();

            if (images.Any())
            {
                return images;
            }

            return null;
        }

        // Update

        // Delete
        public async Task DeleteImagesForParticularMovieAsync(int movieId)
        {
            IQueryable<Image> images = this.dbContext.Images.Where(i => i.MovieId == movieId);

            if (images.Any())
            {
                foreach (var image in images)
                {
                    image.IsDeleted = true;
                    image.DeletedOn = DateTime.UtcNow;
                }

                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            Image image = await this.dbContext.Images.FindAsync(id);

            if (image == null)
            {
                throw new NullReferenceException();
            }

            image.IsDeleted = true;
            image.DeletedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
        }
    }
}
