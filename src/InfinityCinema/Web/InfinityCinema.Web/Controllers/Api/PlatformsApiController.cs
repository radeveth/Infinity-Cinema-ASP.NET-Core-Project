namespace InfinityCinema.Web.Controllers.Api
{
    using System.Collections.Generic;

    using InfinityCinema.Services.Data.PlatformsService;
    using InfinityCinema.Services.Data.PlatformsService.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/platforms/")]
    public class PlatformsApiController : BaseController
    {
        private readonly IPlatformService platformService;

        public PlatformsApiController(IPlatformService platformService)
        {
            this.platformService = platformService;
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<PlatformViewModel>> All(string searchName)
        {
            return this.Json(this.platformService.All<PlatformViewModel>(searchName));
        }
    }
}
