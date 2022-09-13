namespace InfinityCinema.Web.Controllers
{
    using System.Threading.Tasks;

    using InfinityCinema.Common;
    using InfinityCinema.Services.Data.ImagesService;
    using InfinityCinema.Services.Data.ImagesService.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ImagesController : BaseController
    {
        private readonly IImageService imageService;

        public ImagesController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteAsync(int imageId, int movieId)
        {
            await this.imageService.DeleteAsync(imageId);

            return this.RedirectToAction("EditMovieImages", "Movies", new { movieId = movieId });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> CreateAsync(ImageFormModel imageForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("EditMovieImages", "Movies", new { movieId = imageForm.MovieId, newImage = imageForm });
            }

            await this.imageService.CreateAsync<ImageViewModel>(imageForm);

            return this.RedirectToAction("EditMovieImages", "Movies", new { movieId = imageForm.MovieId });
        }
    }
}
