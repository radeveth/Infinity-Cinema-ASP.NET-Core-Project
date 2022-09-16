namespace InfinityCinema.Web.Controllers
{
    using System.Threading.Tasks;

    using InfinityCinema.Common;
    using InfinityCinema.Services.Data.MoviePlatformsService;
    using InfinityCinema.Services.Data.PlatformsService;
    using InfinityCinema.Services.Data.PlatformsService.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PlatformsController : BaseController
    {
        private readonly IPlatformService platformService;
        private readonly IMoviePlatformService moviePlatformService;

        public PlatformsController(IPlatformService platformService, IMoviePlatformService moviePlatformService)
        {
            this.platformService = platformService;
            this.moviePlatformService = moviePlatformService;
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteAsync(int platformId, int movieId)
        {
            await this.moviePlatformService.RemoveRelationBetweenMoviePlatformsAndPlatformsTablesAsync(platformId, movieId);

            await this.platformService.DeleteAsync(platformId);

            return this.RedirectToAction("EditMoviePlatforms", "Movies", new { movieId = movieId });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> CreateAsync(PlatformFormModel platformForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("EditMoviePlatforms", "Movies", new { newPlatform = platformForm, movieId = platformForm.MovieId });
            }

            PlatformViewModel platform = this.platformService.GetViewModelByName<PlatformViewModel>(platformForm.Name);

            if (platform == null)
            {
                platform = await this.platformService.CreateAsync<PlatformViewModel>(platformForm);
            }

            await this.moviePlatformService.CreateRowForMappingTableMoviePlatformsAsync(platformForm.MovieId, platform.Id);

            return this.RedirectToAction("EditMoviePlatforms", "Movies", new { movieId = platformForm.MovieId });
        }
    }
}
