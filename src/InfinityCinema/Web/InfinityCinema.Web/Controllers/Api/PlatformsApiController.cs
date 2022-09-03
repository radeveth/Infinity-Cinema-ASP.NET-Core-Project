namespace InfinityCinema.Web.Controllers.Api
{
    using InfinityCinema.Services.Data.PlatformsService;
    using InfinityCinema.Services.Data.PlatformsService.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

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
            return this.Json(this.platformService.All(searchName));
        }
    }
}
