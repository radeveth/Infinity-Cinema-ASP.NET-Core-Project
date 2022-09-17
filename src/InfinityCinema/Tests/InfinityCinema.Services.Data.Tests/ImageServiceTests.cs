namespace InfinityCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InfinityCinema.Data;
    using InfinityCinema.Data.Models;
    using InfinityCinema.Services.Data.ImagesService;
    using InfinityCinema.Services.Data.ImagesService.Models;
    using InfinityCinema.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ImageServiceTests
    {
        public ImageServiceTests()
        {
            this.InitializeMapper();
        }

        // Test #1
        [Fact]
        public async Task CreateAsyncShouldMapImageFormModelToImageEntityAndAddNewImageToDatabse()
        {
            // Arrange
            int expectedGenresCountInDatabase = 1;
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(1));
            ImageService imageService = new ImageService(dbContext);

            // Act
            await imageService.CreateAsync<ImageViewModel>(new ImageFormModel()
            {
                ImageUrl = "https://thumbs.dreamstime.com/b/cool-wallpapers-backgrounds-check-out-our-68126782.jpg",
                MovieId = 1,
            });

            // Assert
            Assert.Equal(expectedGenresCountInDatabase, dbContext.Images.Count());
        }

        // Test #2
        [Fact]
        public async Task GetImagesForGivenMovieTest()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(2));
            ImageService imageService = new ImageService(dbContext);

            List<Image> images = this.SeedImages();
            await dbContext.Images.AddRangeAsync(images);
            await dbContext.SaveChangesAsync();

            int movieId = 1;
            IEnumerable<string> expectedImages = images.Where(i => i.MovieId == movieId).Select(i => i.Url);

            // Act
            IEnumerable<string> actualImages = imageService.GetImagesForGivenMovie(movieId);

            // Assert
            Assert.Equal(expectedImages.Count(), actualImages.Count());
        }

        // Test #3
        [Fact]
        public async Task GetViewModelByIdTest()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(3));
            ImageService imageService = new ImageService(dbContext);

            List<Image> images = this.SeedImages();
            await dbContext.Images.AddRangeAsync(images);
            await dbContext.SaveChangesAsync();

            // Act
            ImageViewModel imageViewModel = imageService.GetViewModelById<ImageViewModel>(1);

            // Assert
            Assert.Equal(imageViewModel.Url, images.First().Url);
        }

        // Test #4
        [Fact]
        public async Task GetViewModelByMovieIdTest()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(4));
            ImageService imageService = new ImageService(dbContext);

            List<Image> images = this.SeedImages();
            await dbContext.Images.AddRangeAsync(images);
            await dbContext.SaveChangesAsync();

            int expectedImagesCount = images.Count(i => i.MovieId == 1);

            // Act
            IEnumerable<ImageViewModel> imageViewModels = imageService.GetViewModelByMovieId<ImageViewModel>(1);

            // Assert
            Assert.Equal(expectedImagesCount, imageViewModels.Count());
        }

        // Test #5
        [Fact]
        public async Task DeleteImagesForParticularMovieTest()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(5));
            ImageService imageService = new ImageService(dbContext);

            List<Image> images = this.SeedImages();
            await dbContext.Images.AddRangeAsync(images);
            await dbContext.SaveChangesAsync();

            int movieId = 2;
            int countOfImagesWithTargretMovieId = images.Count(i => i.MovieId == movieId);

            // Act
            await imageService.DeleteImagesForParticularMovieAsync(movieId);

            // Assert
            Assert.Equal(images.Count - countOfImagesWithTargretMovieId, dbContext.Images.Count());
        }

        // Test #6
        [Fact]
        public async Task DeleteAsyncTest()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(6));
            ImageService imageService = new ImageService(dbContext);

            List<Image> images = this.SeedImages();
            await dbContext.Images.AddRangeAsync(images);
            await dbContext.SaveChangesAsync();

            int expectedImagesCountInDatabase = images.Count - 1;

            // Act
            await imageService.DeleteAsync(1);

            // Assert
            Assert.Equal(expectedImagesCountInDatabase, dbContext.Images.Count());
        }

        // Test #7
        [Fact]
        public async Task DeleteAsyncShouldThrowNullReferenceExceptionIfImageWithGivenIdDoesNotExist()
        {
            // Arrange
            InfinityCinemaDbContext dbContext = new InfinityCinemaDbContext(this.PrepareOptionsForDbContext(7));
            ImageService imageService = new ImageService(dbContext);

            List<Image> images = this.SeedImages();
            await dbContext.Images.AddRangeAsync(images);
            await dbContext.SaveChangesAsync();

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await imageService.DeleteAsync(images.Count + 100));
        }

        private List<Image> SeedImages()
        {
            return new List<Image>()
            {
                new Image()
                {
                    Url = "https://thumbs.dreamstime.com/b/cool-wallpapers-backgrounds-check-out-our-68126782.jpg1",
                    MovieId = 1,
                },
                new Image()
                {
                    Url = "https://thumbs.dreamstime.com/b/cool-wallpapers-backgrounds-check-out-our-68126782.jpg2",
                    MovieId = 1,
                },
                new Image()
                {
                    Url = "https://thumbs.dreamstime.com/b/cool-wallpapers-backgrounds-check-out-our-68126782.jpg3",
                    MovieId = 2,
                },
                new Image()
                {
                    Url = "https://thumbs.dreamstime.com/b/cool-wallpapers-backgrounds-check-out-our-68126782.jpg4",
                    MovieId = 2,
                },
                new Image()
                {
                    Url = "https://thumbs.dreamstime.com/b/cool-wallpapers-backgrounds-check-out-our-68126782.jpg5",
                    MovieId = 3,
                },
                new Image()
                {
                    Url = "https://thumbs.dreamstime.com/b/cool-wallpapers-backgrounds-check-out-our-68126782.jpg6",
                    MovieId = 3,
                },
            };
        }

        private DbContextOptions<InfinityCinemaDbContext> PrepareOptionsForDbContext(int testCount)
        {
            return new DbContextOptionsBuilder<InfinityCinemaDbContext>()
                .UseInMemoryDatabase(databaseName: $"ImageServiceTestDb{testCount}")
                .Options;
        }

        private void InitializeMapper() => AutoMapperConfig.RegisterMappings(typeof(ImageViewModel).Assembly);
    }
}
