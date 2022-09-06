namespace InfinityCinema.Web.Controllers.Api
{
    using System.Collections.Generic;

    using InfinityCinema.Services.Data.ActorsService;
    using InfinityCinema.Services.Data.ActorsService.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/actors/")]
    public class ActorsApiController : BaseController
    {
        private readonly IActorService actorService;

        public ActorsApiController(IActorService actorService)
        {
            this.actorService = actorService;
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<ActorViewModel>> All(string searchName)
        {
            return this.Json(this.actorService.All(searchName));
        }
    }
}
