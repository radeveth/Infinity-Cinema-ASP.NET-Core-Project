namespace InfinityCinema.Web.Controllers
{
    using System.Threading.Tasks;

    using InfinityCinema.Common;
    using InfinityCinema.Services.Data.PlatformsService;
    using InfinityCinema.Services.Data.PlatformsService.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PlatformsController : BaseController
    {
        private readonly IPlatformService platformService;

        public PlatformsController(IPlatformService platformService)
        {
            this.platformService = platformService;
        }

        public async Task<IActionResult> DeleteAsync(int platformId, int movieId)
        {
            await this.platformService.RemoveRelationBetweenMoviePlatformsAndPlatformsTablesAsync(platformId, movieId);

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

            await this.platformService.CreateRowForMappingTableMoviePlatformsAsync(platformForm.MovieId, platform.Id);

            return this.RedirectToAction("EditMoviePlatforms", "Movies", new { movieId = platformForm.MovieId });
        }
    }
}
